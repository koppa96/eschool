using System;
using System.Threading.Tasks;
using ESchool.ClassRegister.Interface.IntegrationEvents.UserModification;
using ESchool.Testing.Domain;
using ESchool.Testing.Domain.Entities.Users;
using MassTransit;

namespace ESchool.Testing.Application.Features.Users
{
    public class UserModifiedConsumer : IConsumer<UserModifiedEvent>
    {
        private readonly Lazy<TestingContext> lazyDbContext;

        public UserModifiedConsumer(Lazy<TestingContext> lazyDbContext)
        {
            this.lazyDbContext = lazyDbContext;
        }
        
        public async Task Consume(ConsumeContext<UserModifiedEvent> context)
        {
            var dbContext = lazyDbContext.Value;
            var user = await dbContext.Users.FindAsync(context.Message.Id);

            if (user == null)
            {
                user = new TestingUser
                {
                    Id = context.Message.Id
                };
                dbContext.Users.Add(user);
            }

            user.Name = context.Message.Name;
            await dbContext.SaveChangesAsync();
        }
    }
}