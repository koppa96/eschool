using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities.Users.Abstractions;
using ESchool.Libs.Application.Cqrs.Handlers;
using ESchool.Libs.Domain.Enums;
using ESchool.Libs.Interface.Query;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.Users
{
    public class UserListQuery : PagedListQuery<ClassRegisterUserListResponse>
    {
        public string SearchText { get; set; }
    }

    public class ClassRegisterUserListResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        
        public List<TenantRoleType> Roles { get; set; }
    }

    public class UserListHandler : AutoMapperPagedListHandler<UserListQuery, ClassRegisterUser, ClassRegisterUserListResponse>
    {
        public UserListHandler(ClassRegisterContext context, IConfigurationProvider configurationProvider)
            : base(context, configurationProvider)
        {
        }

        protected override IQueryable<ClassRegisterUser> Filter(IQueryable<ClassRegisterUser> entities, UserListQuery query)
        {
            return !string.IsNullOrEmpty(query.SearchText)
                ? entities.Where(x => x.Name.ToLower().Contains(query.SearchText.ToLower()))
                : entities;
        }

        protected override IOrderedQueryable<ClassRegisterUser> Order(IQueryable<ClassRegisterUser> entities, UserListQuery query)
        {
            return entities.OrderBy(x => x.Name);
        }
    }
}