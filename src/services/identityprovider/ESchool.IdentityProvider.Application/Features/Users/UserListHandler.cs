using System.Linq;
using AutoMapper;
using ESchool.IdentityProvider.Application.Features.Users.Common;
using ESchool.IdentityProvider.Domain;
using ESchool.IdentityProvider.Domain.Entities.Users;
using ESchool.Libs.Application.Cqrs.Handlers;
using ESchool.Libs.Application.Cqrs.Query;

namespace ESchool.IdentityProvider.Application.Features.Users
{
    public class UserListQuery : PagedListQuery<UserListResponse>
    {
    }

    public class UserListHandler : AutoMapperPagedListHandler<UserListQuery, User, UserListResponse>
    {
        public UserListHandler(IdentityProviderContext context, IConfigurationProvider configurationProvider) : base(context, configurationProvider)
        {
        }
        
        protected override IOrderedQueryable<User> Order(IQueryable<User> entities)
        {
            return entities.OrderBy(x => x.Email);
        }
    }
}