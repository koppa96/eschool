using System;
using System.Linq;
using System.Linq.Expressions;
using ESchool.IdentityProvider.Application.Features.Users.Common;
using ESchool.IdentityProvider.Domain;
using ESchool.IdentityProvider.Domain.Entities.Users;
using ESchool.Libs.Application.Cqrs.Handlers;
using ESchool.Libs.Application.Cqrs.Query;

namespace ESchool.IdentityProvider.Application.Features.TenantUsers
{
    public class TenantUserListQuery : PagedListQuery<UserListResponse>
    {
        public Guid TenantId { get; set; }
    }

    public class TenantUserListHandler : PagedListHandler<TenantUserListQuery, User, string, UserListResponse>
    {
        public TenantUserListHandler(IdentityProviderContext context) : base(context)
        {
        }

        protected override Expression<Func<User, string>> OrderBy => x => x.Email;

        protected override Expression<Func<User, UserListResponse>> Select => x => new UserListResponse
        {
            Id = x.Id,
            Email = x.Email,
            GlobalRoleType = x.GlobalRole
        };

        protected override IQueryable<User> Filter(IQueryable<User> entities, TenantUserListQuery query)
        {
            return entities.Where(x => x.TenantUsers.Any(u => u.TenantId == query.TenantId));
        }
    }
}