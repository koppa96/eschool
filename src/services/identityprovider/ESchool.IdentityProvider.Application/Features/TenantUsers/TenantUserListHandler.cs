using System.Linq;
using AutoMapper;
using ESchool.IdentityProvider.Domain;
using ESchool.IdentityProvider.Domain.Entities.Users;
using ESchool.IdentityProvider.Interface.Features.TenantUsers;
using ESchool.IdentityProvider.Interface.Features.Users;
using ESchool.Libs.Application.Cqrs.Handlers;

namespace ESchool.IdentityProvider.Application.Features.TenantUsers
{
    public class TenantUserListHandler : AutoMapperPagedListHandler<TenantUserListQuery, User, UserListResponse>
    {
        public TenantUserListHandler(IdentityProviderContext context, IConfigurationProvider configurationProvider) : base(context, configurationProvider)
        {
        }

        protected override IQueryable<User> Filter(IQueryable<User> entities, TenantUserListQuery query)
        {
            return entities.Where(x => x.TenantUsers.Any(u => u.TenantId == query.TenantId));
        }

        protected override IOrderedQueryable<User> Order(IQueryable<User> entities)
        {
            return entities.OrderBy(x => x.Email);
        }
    }
}