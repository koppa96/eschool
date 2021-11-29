using System;
using System.Linq;
using ESchool.IdentityProvider.Domain;
using ESchool.IdentityProvider.Domain.Entities.Users;
using ESchool.IdentityProvider.Interface.Features.TenantUsers;
using ESchool.Libs.Application.Cqrs.Handlers;
using Microsoft.EntityFrameworkCore;

namespace ESchool.IdentityProvider.Application.Features.TenantUsers
{
    public class TenantUserListHandler : PagedListHandler<TenantUserListQuery, User, TenantUserListResponse>
    {
        public TenantUserListHandler(IdentityProviderContext context) : base(context)
        {
        }

        protected override IQueryable<User> Include(IQueryable<User> entities)
        {
            return entities.Include(x => x.TenantUsers)
                .ThenInclude(x => x.TenantUserRoles);
        }

        protected override IQueryable<User> Filter(IQueryable<User> entities, TenantUserListQuery query)
        {
            return entities.Where(x => x.TenantUsers.Any(tu => tu.TenantId == query.TenantId));
        }

        protected override IQueryable<TenantUserListResponse> Map(IQueryable<User> entities, TenantUserListQuery query)
        {
            return entities.Select(x => new TenantUserListResponse
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                GlobalRole = x.GlobalRole,
                TenantRoleTypes = x.TenantUsers
                    .Single(tu => tu.TenantId == query.TenantId)
                    .TenantUserRoles
                        .Select(tur => tur.TenantRole)
                        .ToList()
            });
        }

        protected override IOrderedQueryable<User> Order(IQueryable<User> entities, TenantUserListQuery query)
        {
            return entities.OrderBy(x => x.Name);
        }
    }
}