using System;
using System.Threading.Tasks;
using ESchool.ClassRegister.Grpc;
using ESchool.ClassRegister.Interface.IntegrationEvents.UserModification;
using ESchool.HomeAssignments.Domain;
using ESchool.HomeAssignments.Domain.Entities.Users;
using MassTransit;

namespace ESchool.HomeAssignments.Application.Features.Users
{
    public class UserModifiedConsumer : IConsumer<UserModifiedEvent>
    {
        private readonly Lazy<HomeAssignmentsContext> lazyDbContext;

        public UserModifiedConsumer(Lazy<HomeAssignmentsContext> lazyDbContext)
        {
            this.lazyDbContext = lazyDbContext;
        }
        
        public async Task Consume(ConsumeContext<UserModifiedEvent> context)
        {
            var dbContext = lazyDbContext.Value;
            var user = await dbContext.Users.FindAsync(context.Message.Id);

            if (user == null)
            {
                user = new HomeAssignmentsUser
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