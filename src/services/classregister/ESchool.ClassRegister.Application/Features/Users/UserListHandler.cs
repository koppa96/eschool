using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities.Users;
using ESchool.ClassRegister.Domain.Entities.Users.Abstractions;
using ESchool.ClassRegister.Interface.Features.Users;
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
    
    public class UserListHandler : PagedListHandler<UserListQuery, ClassRegisterUser, ClassRegisterUserListResponse>
    {
        public UserListHandler(ClassRegisterContext context)
            : base(context)
        {
        }

        protected override IQueryable<ClassRegisterUser> Filter(IQueryable<ClassRegisterUser> entities, UserListQuery query)
        {
            return !string.IsNullOrEmpty(query.SearchText)
                ? entities.Where(x => x.Name.ToLower().Contains(query.SearchText.ToLower()))
                : entities;
        }

        protected override IQueryable<ClassRegisterUserListResponse> Map(IQueryable<ClassRegisterUser> entities, UserListQuery query)
        {
            return entities.Select(x => new ClassRegisterUserListResponse
            {
                Id = x.Id,
                Name = x.Name,
                Roles = x.UserRoles.Select(x => x is Administrator
                    ? TenantRoleType.Administrator
                    : x is Teacher
                        ? TenantRoleType.Teacher
                        : x is Student
                            ? TenantRoleType.Student
                            : x is Parent
                                ? TenantRoleType.Parent
                                : default).ToList()
            });
        }

        protected override IOrderedQueryable<ClassRegisterUser> Order(IQueryable<ClassRegisterUser> entities, UserListQuery query)
        {
            return entities.OrderBy(x => x.Name);
        }
    }
}