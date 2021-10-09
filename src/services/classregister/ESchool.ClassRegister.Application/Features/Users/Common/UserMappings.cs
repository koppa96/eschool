﻿using AutoMapper;
using ESchool.ClassRegister.Domain.Entities.Users;
using ESchool.ClassRegister.Domain.Entities.Users.Abstractions;
using ESchool.ClassRegister.Interface.Features.Users;
using ESchool.ClassRegister.Interface.IntegrationEvents.UserCreation;
using ESchool.ClassRegister.Interface.IntegrationEvents.UserDeletion;
using ESchool.Libs.Domain.Enums;
using ESchool.Libs.Interface.Response.Common;

namespace ESchool.ClassRegister.Application.Features.Users.Common
{
    public class UserMappings : Profile
    {
        public UserMappings()
        {
            CreateMap<ClassRegisterUserRole, UserRoleListResponse>()
                .ForMember(x => x.Name, o => o.MapFrom(x => x.User.Name));

            CreateMap<Teacher, TenantUserRoleDeletedEvent>()
                .ConstructUsing(teacher => new TeacherDeletedEvent());

            CreateMap<Student, TenantUserRoleDeletedEvent>()
                .ConstructUsing(student => new StudentDeletedEvent());

            CreateMap<Teacher, TenantUserRoleCreatedEvent>()
                .ConstructUsing(teacher => new TeacherCreatedEvent
                {
                    Id = teacher.Id,
                    Name = teacher.User.Name,
                    UserId = teacher.UserId
                });

            CreateMap<Student, TenantUserRoleCreatedEvent>()
                .ConstructUsing(student => new StudentCreatedEvent
                {
                    Id = student.Id,
                    Name = student.User.Name,
                    UserId = student.UserId
                });

            CreateMap<ClassRegisterUser, ClassRegisterUserListResponse>()
                .ForMember(x => x.Roles, o => o.MapFrom(x => x.UserRoles));

            CreateMap<Teacher, TenantRoleType>()
                .ConstructUsing(_ => TenantRoleType.Teacher);

            CreateMap<Student, TenantRoleType>()
                .ConstructUsing(_ => TenantRoleType.Student);

            CreateMap<Administrator, TenantRoleType>()
                .ConstructUsing(_ => TenantRoleType.Administrator);

            CreateMap<Parent, TenantRoleType>()
                .ConstructUsing(_ => TenantRoleType.Parent);
        }
    }
}