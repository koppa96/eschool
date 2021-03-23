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
        private readonly UserService.UserServiceClient client;

        public UserModifiedConsumer(Lazy<HomeAssignmentsContext> lazyDbContext, UserService.UserServiceClient client)
        {
            this.lazyDbContext = lazyDbContext;
            this.client = client;
        }
        
        public async Task Consume(ConsumeContext<UserModifiedEvent> context)
        {
            var dbContext = lazyDbContext.Value;
            var user = await dbContext.Users.FindAsync(context.Message.Id);
            var modifiedUser = await client.GetUserAsync(new IdentityUserGetRequest
            {
                Id = context.Message.Id.ToString()
            });

            if (user == null)
            {
                user = new HomeAssignmentsUser
                {
                    Id = context.Message.Id
                };
                dbContext.Users.Add(user);
            }

            user.Name = modifiedUser.Name;
            await dbContext.SaveChangesAsync();
        }
    }
}