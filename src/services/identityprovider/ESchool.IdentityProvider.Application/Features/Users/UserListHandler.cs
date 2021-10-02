using System.Linq;
using AutoMapper;
using ESchool.IdentityProvider.Domain;
using ESchool.IdentityProvider.Domain.Entities.Users;
using ESchool.IdentityProvider.Interface.Features.Users;
using ESchool.Libs.Application.Cqrs.Handlers;

namespace ESchool.IdentityProvider.Application.Features.Users
{
    public class UserListHandler : AutoMapperPagedListHandler<UserListQuery, User, UserListResponse>
    {
        public UserListHandler(IdentityProviderContext context, IConfigurationProvider configurationProvider) : base(context, configurationProvider)
        {
        }
        
        protected override IOrderedQueryable<User> Order(IQueryable<User> entities, UserListQuery query)
        {
            return entities.OrderBy(x => x.Email);
        }
    }
}