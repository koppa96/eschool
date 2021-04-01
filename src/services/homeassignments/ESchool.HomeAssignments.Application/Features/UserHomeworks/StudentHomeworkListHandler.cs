using System;
using System.Linq;
using AutoMapper;
using ESchool.HomeAssignments.Domain;
using ESchool.HomeAssignments.Domain.Entities;
using ESchool.Libs.Application.Cqrs.Handlers;
using ESchool.Libs.Application.Cqrs.Query;
using ESchool.Libs.Domain.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.HomeAssignments.Application.Features.UserHomeworks
{
    public class StudentHomeworkListQuery : PagedListQuery<StudentHomeworkListResponse>
    {
        public Guid SchoolYearId { get; set; }
        public Guid SubjectId { get; set; }
        public bool Expired { get; set; }
    }

    public class StudentHomeworkListResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime Deadline { get; set; }
        public bool Submitted { get; set; }
        public bool Optional { get; set; }
    }
    
    public class StudentHomeworkListHandler : PagedListHandler<StudentHomeworkListQuery, Homework, StudentHomeworkListResponse>
    {
        private readonly Guid currentUserId;

        public StudentHomeworkListHandler(
            HomeAssignmentsContext context,
            IIdentityService identityService) : base(context)
        {
            currentUserId = identityService.GetCurrentUserId();
        }

        protected override IQueryable<Homework> Filter(IQueryable<Homework> entities, StudentHomeworkListQuery query)
        {
            return entities.Where(x =>
                x.Lesson.ClassSchoolYearSubject.ClassSchoolYearSubjectStudents.Any(s =>
                    s.Student.UserId == currentUserId));
        }

        protected override IQueryable<StudentHomeworkListResponse> Map(IQueryable<Homework> entities, StudentHomeworkListQuery query)
        {
            return entities.Select(x => new StudentHomeworkListResponse
            {
                Id = x.Id,
                Title = x.Title,
                Deadline = x.Deadline,
                Optional = x.Optional,
                Submitted = x.Solutions.Any(s => s.Student.UserId == currentUserId)
            });
        }

        protected override IOrderedQueryable<Homework> Order(IQueryable<Homework> entities)
        {
            return entities.OrderByDescending(x => x.Deadline);
        }
    }
}