using System;
using System.Linq;
using AutoMapper;
using ESchool.HomeAssignments.Application.Features.Users.Common;
using ESchool.HomeAssignments.Domain;
using ESchool.HomeAssignments.Domain.Entities;
using ESchool.Libs.Application.Cqrs.Handlers;
using ESchool.Libs.Application.Cqrs.Query;
using Microsoft.EntityFrameworkCore;

namespace ESchool.HomeAssignments.Application.Features.UserHomeworks
{
    public class TeacherHomeworkListQuery : PagedListQuery<UserListResponse>
    {
        public Guid HomeworkId { get; set; }
    }
    
    public class TeacherHomeworkListHandler : AutoMapperPagedListHandler<TeacherHomeworkListQuery, TeacherHomework, UserListResponse>
    {
        public TeacherHomeworkListHandler(HomeAssignmentsContext context,
            IConfigurationProvider configurationProvider) : base(context, configurationProvider)
        {
        }

        protected override IOrderedQueryable<TeacherHomework> Order(IQueryable<TeacherHomework> entities)
        {
            return entities.OrderBy(x => x.Teacher.User.Name);
        }
    }
}