using System;
using System.Linq;
using AutoMapper;
using ESchool.HomeAssignments.Application.Features.Users.Common;
using ESchool.HomeAssignments.Domain;
using ESchool.HomeAssignments.Domain.Entities;
using ESchool.HomeAssignments.Domain.Entities.Users;
using ESchool.HomeAssignments.Domain.Enums;
using ESchool.Libs.Application.Cqrs.Handlers;
using ESchool.Libs.Application.Cqrs.Query;

namespace ESchool.HomeAssignments.Application.Features.UserHomeworks
{
    public class StudentHomeworkListQuery : PagedListQuery<StudentHomeworkListResponse>
    {
        public Guid HomeworkId { get; set; }
    }

    public class StudentHomeworkListResponse
    {
        public UserListResponse Student { get; set; }
        public bool IsSubmitted { get; set; }
        public HomeworkReviewOutcome? HomeworkReviewOutcome { get; set; }
    }
    
    public class StudentHomeworkListHandler : AutoMapperPagedListHandler<StudentHomeworkListQuery, Student, StudentHomeworkListResponse>
    {
        public StudentHomeworkListHandler(HomeAssignmentsContext context, IConfigurationProvider configurationProvider) : base(context, configurationProvider)
        {
        }

        protected override IQueryable<Student> Filter(IQueryable<Student> entities, StudentHomeworkListQuery query)
        {
            return entities.Where(x => x.ClassSchoolYearSubjectStudents.SelectMany(x => x.ClassSchoolYearSubject.Lessons.SelectMany(x => x.HomeWorks)).);
        }

        protected override IOrderedQueryable<Student> Order(IQueryable<Student> entities)
        {
            return entities.OrderBy(x => x.User.Name);
        }
    }
}