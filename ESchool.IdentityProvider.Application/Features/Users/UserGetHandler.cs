using ESchool.IdentityProvider.Domain;
using ESchool.Libs.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ESchool.IdentityProvider.Application.Features.Users
{
    public class UserGetQuery : IRequest<UserGetResponse>
    {
        public Guid Id { get; set; }
    }

    public class UserGetResponse
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public GlobalRoleType GlobalRoleType { get; set; }
        public List<TenantUserResponse> Tenants { get; set; }
        public Guid? DefaultTenantId { get; set; }

        public class TenantUserResponse
        {
            public Guid TenantId { get; set; }
            public string Name { get; set; }
            public List<TenantRoleType> TenantRoleTypes { get; set; }
        }
    }

    public class UserGetHandler : IRequestHandler<UserGetQuery, UserGetResponse>
    {
        private readonly IdentityProviderContext context;

        public UserGetHandler(IdentityProviderContext context)
        {
            this.context = context;
        }

        public async Task<UserGetResponse> Handle(UserGetQuery request, CancellationToken cancellationToken)
        {
            var user = await context.Users.Include(x => x.TenantUsers)
                    .ThenInclude(x => x.Tenant)
                .Include(x => x.TenantUsers)
                    .ThenInclude(x => x.TenantUserRoles)
                .SingleAsync(x => x.Id == request.Id);

            return new UserGetResponse
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                DefaultTenantId = user.DefaultTenantId,
                GlobalRoleType = user.GlobalRole,
                Tenants = user.TenantUsers.Select(x => new UserGetResponse.TenantUserResponse
                {
                    TenantId = x.TenantId,
                    Name = x.Tenant.Name,
                    TenantRoleTypes = x.TenantUserRoles.Select(ur => ur.TenantRole).ToList()
                }).ToList()
            };
        }
    }
}
