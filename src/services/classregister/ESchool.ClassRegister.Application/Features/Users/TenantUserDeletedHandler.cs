using System.Linq;
using System.Threading.Tasks;
using ESchool.ClassRegister.Domain;
using ESchool.IdentityProvider.Interface.IntegrationEvents.TenantUsers;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.Users
{
    public class TenantUserDeletedHandler : IConsumer<TenantUserDeletedEvent>
    {
        private readonly ClassRegisterContext dbContext;

        public TenantUserDeletedHandler(ClassRegisterContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Consume(ConsumeContext<TenantUserDeletedEvent> context)
        {
            var userBases = await dbContext.UserBases.IgnoreQueryFilters()
                .Where(x => x.UserId == context.Message.UserId)
                .ToListAsync();

            dbContext.UserBases.RemoveRange(userBases);
            await dbContext.SaveChangesAsync();
        }
    }
}