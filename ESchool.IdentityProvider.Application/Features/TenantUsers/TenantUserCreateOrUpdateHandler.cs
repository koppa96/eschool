using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESchool.IdentityProvider.Domain;
using ESchool.IdentityProvider.Domain.Entities.Users;
using ESchool.Libs.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.IdentityProvider.Application.Features.TenantUsers
{
    public class TenantUserCreateOrUpdateCommand : IRequest
    {
        public Guid TenantId { get; set; }
        public Guid UserId { get; set; }
        public List<TenantRoleType> TenantRoleTypes { get; set; }
    }
    
    public class TenantUserCreateOrUpdateHandler : IRequestHandler<TenantUserCreateOrUpdateCommand, Unit>
    {
        private readonly IdentityProviderContext context;

        public TenantUserCreateOrUpdateHandler(IdentityProviderContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(TenantUserCreateOrUpdateCommand request, CancellationToken cancellationToken)
        {
            var tenantUser = await context.TenantUsers.Include(x => x.TenantUserRoles)
                    .SingleOrDefaultAsync(x => x.TenantId == request.TenantId && x.UserId == request.UserId, cancellationToken);

            if (tenantUser == null)
            {
                context.TenantUsers.Add(new TenantUser
                {
                    TenantId = request.TenantId,
                    UserId = request.UserId,
                    TenantUserRoles = request.TenantRoleTypes.Select(x => new TenantUserRole
                    {
                        TenantRole = x
                    }).ToList()
                });
            }
            else
            {
                context.TenantUserRoles.RemoveRange(tenantUser.TenantUserRoles);
                context.TenantUserRoles.AddRange(request.TenantRoleTypes.Select(x => new TenantUserRole
                {
                    TenantUserId = tenantUser.Id,
                    TenantRole = x
                }));
            }

            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}