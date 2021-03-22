using System;
using System.Linq;
using AutoMapper;
using ESchool.HomeAssignments.Domain;
using ESchool.HomeAssignments.Domain.Entities;
using ESchool.HomeAssignments.Domain.Enums;
using ESchool.Libs.Application.Cqrs.Handlers;
using ESchool.Libs.Application.Cqrs.Query;
using ESchool.Libs.Domain.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.HomeAssignments.Application.Features.Homeworks
{
    public class HomeworkListAsStudentQuery : PagedListQuery<HomeworkListAsStudentResponse>
    {
        public Guid SchoolYearId { get; set; }
        public Guid SubjectId { get; set; }
        public bool Expired { get; set; }
    }

    public class HomeworkListAsStudentResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime Deadline { get; set; }
        public bool Submitted { get; set; }
        public HomeworkReviewOutcome? Outcome { get; set; }
    }

    public class HomeworkListAsStudentHandler : PagedListHandler<HomeworkListAsStudentQuery, StudentHomework,
            HomeworkListAsStudentResponse>
    {
        private readonly IIdentityService identityService;

        public HomeworkListAsStudentHandler(HomeAssignmentsContext context, IIdentityService identityService) :
            base(context)
        {
            this.identityService = identityService;
        }

        protected override IQueryable<StudentHomework> Filter(IQueryable<StudentHomework> entities, HomeworkListAsStudentQuery query)
        {
            var now = DateTime.Now;
            var currentUserId = identityService.GetCurrentUserId();
            return entities.Where(x => x.Student.UserId == currentUserId && 
                                       x.Homework.Lesson.SchoolYearId == query.SchoolYearId &&
                                       x.Homework.Lesson.SubjectId == query.SubjectId &&
                                       x.Homework.StudentHomeworks.Any(sh => sh.Student.UserId == currentUserId) &&
                                       (query.Expired ? now > x.Homework.Deadline : now <= x.Homework.Deadline));
        }

        protected override IQueryable<HomeworkListAsStudentResponse> Map(IQueryable<StudentHomework> entities,
            HomeworkListAsStudentQuery query)
        {
            return entities.Select(x => new HomeworkListAsStudentResponse
            {
                Id = x.Homework.Id,
                Deadline = x.Homework.Deadline,
                Title = x.Homework.Title,
                Submitted = x.HomeworkSolutionId != null,
                Outcome = x.HomeworkSolution != null && x.HomeworkSolution.HomeworkReview != null
                    ? x.HomeworkSolution.HomeworkReview.Outcome
                    : (HomeworkReviewOutcome?) null
            });
        }

        protected override IOrderedQueryable<StudentHomework> Order(IQueryable<StudentHomework> entities)
        {
            return entities.OrderBy(x => x.Homework.Deadline);
        }
    }
}