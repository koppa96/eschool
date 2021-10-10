using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESchool.ClassRegister.Interface.IntegrationEvents.UserCreation;
using ESchool.Testing.Domain;
using ESchool.Testing.Domain.Entities.ClassRegisterData;
using ESchool.Testing.Domain.Entities.Users;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace ESchool.Testing.Application.Features.Users
{
    public class TeacherCreatedConsumer : IConsumer<TeacherCreatedEvent>
    {
        private readonly Lazy<TestingContext> lazyDbContext;

        public TeacherCreatedConsumer(Lazy<TestingContext> lazyDbContext)
        {
            this.lazyDbContext = lazyDbContext;
        }

        public async Task Consume(ConsumeContext<TeacherCreatedEvent> context)
        {
            var dbContext = lazyDbContext.Value;
            var user = await dbContext.Users.Include(x => x.UserRoles)
                .SingleOrDefaultAsync(x => x.Id == context.Message.UserId);
            
            if (user == null)
            {
                user = new TestingUser
                {
                    Id = context.Message.UserId,
                    Name = context.Message.Name,
                    UserRoles = new List<TestingUserRole>()
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