using System;
using System.Linq.Expressions;
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

    public class UserListHandler : PagedListHandler<UserListQuery, User, string, UserListResponse>
    {
        public UserListHandler(IdentityProviderContext context) : base(context)
        {
        }

        protected override Expression<Func<User, string>> OrderBy => x => x.Email;

        protected override Expression<Func<User, UserListResponse>> Select => x => new UserListResponse
        {
            Id = x.Id,
            Email = x.Email,
            GlobalRole = x.GlobalRole
        };
    }
}