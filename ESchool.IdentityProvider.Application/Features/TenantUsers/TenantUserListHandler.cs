using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESchool.IdentityProvider.Application.Features.Users.Common;
using ESchool.IdentityProvider.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.IdentityProvider.Application.Features.TenantUsers
{
    public class TenantUserListQuery : IRequest<List<UserListResponse>>
    {
        public Guid TenantId { get; set; }
    }

    public class TenantUserListHandler : IRequestHandler<TenantUserListQuery, List<UserListResponse>>
    {
        private readonly IdentityProviderContext context;

        public TenantUserListHandler(IdentityProviderContext context)
        {
            this.context = context;
        }
        
        public async Task<List<UserListResponse>> Handle(TenantUserListQuery request, CancellationToken cancellationToken)
        {
            var tenant = await context.Tenants.Include(x => x.TenantUsers)
                    .ThenInclude(x => x.User)
                .SingleAsync(x => x.Id == request.TenantId, cancellationToken);

            return tenant.TenantUsers.Select(x => new UserListResponse
            {
                Id = x.UserId,
                Email = x.User.Email,
                UserName = x.User.UserName,
                GlobalRoleType = x.User.GlobalRole
            }).ToList();
        }
    }
}