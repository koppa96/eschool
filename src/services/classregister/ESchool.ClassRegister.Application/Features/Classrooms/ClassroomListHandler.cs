using System;
using System.Linq.Expressions;
using AutoMapper;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities;
using ESchool.Libs.Application.Cqrs.Handlers;
using ESchool.Libs.Application.Cqrs.Query;

namespace ESchool.ClassRegister.Application.Features.Classrooms
{
    public class ClassroomListQuery : PagedListQuery<ClassroomListResponse>
    {
    }

    public class ClassroomListResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
    
    public class ClassroomListHandler : AutoMapperPagedListHandler<ClassroomListQuery, Classroom, string, ClassroomListResponse>
    {
        public ClassroomListHandler(ClassRegisterContext context, IConfigurationProvider configurationProvider) : base(context, configurationProvider)
        {
        }

        protected override Expression<Func<Classroom, string>> OrderBy => x => x.Name;
    }
}