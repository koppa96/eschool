using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESchool.ClassRegister.Grpc;
using ESchool.ClassRegister.Interface.IntegrationEvents.UserCreation;
using ESchool.HomeAssignments.Domain;
using ESchool.HomeAssignments.Domain.Entities.Users;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace ESchool.HomeAssignments.Application.Features.Users
{
    public class StudentCreatedConsumer : IConsumer<StudentCreatedEvent>
    {
        private readonly Lazy<HomeAssignmentsContext> lazyDbContext;

        public StudentCreatedConsumer(Lazy<HomeAssignmentsContext> lazyDbContext)
        {
            this.lazyDbContext = lazyDbContext;
        }

        public async Task Consume(ConsumeContext<StudentCreatedEvent> context)
        {
            var dbContext = lazyDbContext.Value;
            var user = await dbContext.Users.Include(x => x.UserRoles)
                .SingleOrDefaultAsync(x => x.Id == context.Message.UserId);
            
            if (user == null)
            {
                user = new HomeAssignmentsUser
                {
                    Id = context.Message.UserId,
                    Name = context.Message.Name,
                    UserRoles = new List<HomeAssignmentsUserRole>()
                };
                dbContext.Users.Add(user);
            }
            
            if (user.UserRoles.All(x => x.Id != context.Message.Id))
            {
                dbContext.Students.Add(new Student
                {
                    Id = context.Message.Id,
                    User = user
                });
            }

            await dbContext.SaveChangesAsync();
        }
    }
}