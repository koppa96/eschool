using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Domain;
using ESchool.Libs.Application.IntegrationEvents.TenantUsers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.Users
{
    public class TenantUserDeletedHandler : IRequestHandler<TenantUserDeletedIntegrationEvent>
    {
        private readonly ClassRegisterContext context;

        public TenantUserDeletedHandler(ClassRegisterContext context)
        {
            this.context = context;
        }
        
        public async Task<Unit> Handle(TenantUserDeletedIntegrationEvent request, CancellationToken cancellationToken)
        {
            var userBases = await context.UserBases.IgnoreQueryFilters()
                .Where(x => x.UserId == request.UserId && x.TenantId == request.TenantId)
                .ToListAsync(cancellationToken);

            foreach (var userBase in userBases)
            {
                userBase.IsDeleted = true;
            }

            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}