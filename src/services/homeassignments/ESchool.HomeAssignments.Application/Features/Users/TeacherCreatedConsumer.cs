using System;
using System.Linq;
using ESchool.HomeAssignments.Domain;
using ESchool.HomeAssignments.Domain.Entities.Users;
using System.Threading.Tasks;
using ESchool.ClassRegister.Grpc;
using ESchool.ClassRegister.Interface.IntegrationEvents.UserCreation;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace ESchool.HomeAssignments.Application.Features.Users
{
    public class TeacherCreatedConsumer : IConsumer<TeacherCreatedEvent>
    {
        private readonly Lazy<HomeAssignmentsContext> lazyDbContext;
        private readonly UserService.UserServiceClient client;

        public TeacherCreatedConsumer(Lazy<HomeAssignmentsContext> lazyDbContext, UserService.UserServiceClient client)
        {
            this.lazyDbContext = lazyDbContext;
            this.client = client;
        }

        public async Task Consume(ConsumeContext<TeacherCreatedEvent> context)
        {
            var dbContext = lazyDbContext.Value;
            var user = await dbContext.Users.Include(x => x.UserRoles)
                .SingleOrDefaultAsync(x => x.Id == context.Message.UserId);
            
            if (user == null)
            {
                user = new HomeAssignmentsUser
                {
                    Id = context.Message.UserId,
                    Name = context.Message.Name
                };
                dbContext.Users.Add(user);
            }

            if (user.UserRoles.All(x => x.Id != context.Message.Id))
            {
                dbContext.Teachers.Add(new Teacher
                {
                    Id = context.Message.Id,
                    User = user
                });
            }

            await dbContext.SaveChangesAsync();
        }
    }
}