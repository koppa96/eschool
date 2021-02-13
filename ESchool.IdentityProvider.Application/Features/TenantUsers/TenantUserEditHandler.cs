using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using ESchool.IdentityProvider.Application.Features.TenantUsers.Common;
using ESchool.IdentityProvider.Domain;
using ESchool.IdentityProvider.Domain.Entities.Users;
using ESchool.Libs.Application.Cqrs.Commands;
using ESchool.Libs.Domain.Enums;
using ESchool.Libs.Domain.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.IdentityProvider.Application.Features.TenantUsers
{
    public class TenantUserEditCommand : IRequest<TenantUserDetailsResponse>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public IEnumerable<TenantRoleType> TenantRoleTypes { get; set; }
    }
    
    public class TenantUserEditHandler : IRequestHandler<TenantUserEditCommand, TenantUserDetailsResponse>
    {
        private readonly IdentityProviderContext context;
        private readonly IIdentityService identityService;

        public TenantUserEditHandler(IdentityProviderContext context, IIdentityService identityService)
        {
            this.context = context;
            this.identityService = identityService;
        }
        
        public async Task<TenantUserDetailsResponse> Handle(TenantUserEditCommand request,
            CancellationToken cancellationToken)
        {
            var tenantId = identityService.GetTenantId() ??
                           throw new InvalidOperationException("TenantId can not be null.");
            var tenantUser = await context.TenantUsers.Include(x => x.TenantUserRoles)
                .Include(x => x.User)
                .SingleAsync(x => x.UserId == request.Id && x.TenantId == tenantId, cancellationToken);
            
            context.TenantUserRoles.RemoveRange(tenantUser.TenantUserRoles);
            context.TenantUserRoles.AddRange(request.TenantRoleTypes.Select(x => new TenantUserRole
            {
                TenantUserId = tenantUser.Id,
                TenantRole = x
            }));
            await context.SaveChangesAsync(cancellationToken);
            return new TenantUserDetailsResponse
            {
                Id = tenantUser.UserId,
                Email = tenantUser.User.Email,
                TenantRoleTypes = tenantUser.TenantUserRoles.Select(x => x.TenantRole)
            };
        }
    }
}