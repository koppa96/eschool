using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.IdentityProvider.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.IdentityProvider.Application.Features.TenantUsers
{
    public class TenantUserDeleteCommand : IRequest
    {
        public Guid TenantId { get; set; }
        public Guid UserId { get; set; }
    }
    
    public class TenantUserDeleteHandler : IRequestHandler<TenantUserDeleteCommand, Unit>
    {
        private readonly IdentityProviderContext context;

        public TenantUserDeleteHandler(IdentityProviderContext context)
        {
            this.context = context;
        }
        
        public async Task<Unit> Handle(TenantUserDeleteCommand request, CancellationToken cancellationToken)
        {
            var tenantUser = await context.TenantUsers.SingleOrDefaultAsync(
                x => x.TenantId == request.UserId && x.TenantId == request.TenantId, cancellationToken);

            if (tenantUser != null)
            {
                context.TenantUsers.Remove(tenantUser);
                await context.SaveChangesAsync(cancellationToken);
            }
            
            return Unit.Value;
        }
    }
}