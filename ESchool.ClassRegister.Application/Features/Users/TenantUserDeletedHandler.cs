using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Domain;
using ESchool.Libs.Application.IntegrationEvents.TenantUsers;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.Users
{
    public class TenantUserDeletedHandler : IConsumer<TenantUserDeletedIntegrationEvent>
    {
        private readonly ClassRegisterContext dbContext;

        public TenantUserDeletedHandler(ClassRegisterContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Consume(ConsumeContext<TenantUserDeletedIntegrationEvent> context)
        {
            var userBases = await dbContext.UserBases.IgnoreQueryFilters()
                .Where(x => x.UserId == context.Message.UserId && x.TenantId == context.Message.TenantId)
                .ToListAsync();

            dbContext.UserBases.RemoveRange(userBases);
            await dbContext.SaveChangesAsync();
        }
    }
}