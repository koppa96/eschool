using System;
using System.Linq;
using AutoMapper;
using ESchool.HomeAssignments.Application.Features.Users.Common;
using ESchool.HomeAssignments.Domain;
using ESchool.HomeAssignments.Domain.Entities;
using ESchool.Libs.Application.Cqrs.Handlers;
using ESchool.Libs.Application.Cqrs.Query;

namespace ESchool.HomeAssignments.Application.Features.UserHomeworks
{
    public class StudentHomeworkListQuery : PagedListQuery<UserListResponse>
    {
        public Guid HomeworkId { get; set; }
    }
    
    public class StudentHomeworkListHandler : AutoMapperPagedListHandler<StudentHomeworkListQuery, StudentHomework, UserListResponse>
    {
        public StudentHomeworkListHandler(HomeAssignmentsContext context,
            IConfigurationProvider configurationProvider) : base(context, configurationProvider)
        {
        }

        protected override IQueryable<StudentHomework> Filter(IQueryable<StudentHomework> entities, StudentHomeworkListQuery query)
        {
            return entities.Where(x => x.HomeworkId == query.HomeworkId);
        }

        protected override IOrderedQueryable<StudentHomework> Order(IQueryable<StudentHomework> entities)
        {
            return entities.OrderBy(x => x.Student.Name);
        }
    }
}