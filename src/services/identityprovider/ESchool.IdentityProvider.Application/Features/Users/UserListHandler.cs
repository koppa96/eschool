using System;
using System.Linq.Expressions;
using AutoMapper;
using ESchool.IdentityProvider.Application.Features.Users.Common;
using ESchool.IdentityProvider.Domain;
using ESchool.IdentityProvider.Domain.Entities.Users;
using ESchool.Libs.Application.Cqrs.Handlers;
using ESchool.Libs.Application.Cqrs.Query;
using Microsoft.EntityFrameworkCore;

namespace ESchool.IdentityProvider.Application.Features.Users
{
    public class UserListQuery : PagedListQuery<UserListResponse>
    {
    }

    public class UserListHandler : AutoMapperPagedListHandler<UserListQuery, User, string, UserListResponse>
    {
        public UserListHandler(IdentityProviderContext context, IConfigurationProvider configurationProvider) : base(context, configurationProvider)
        {
        }

        protected override Expression<Func<User, string>> OrderBy => x => x.Email;
    }
}