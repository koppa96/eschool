using System;
using System.Linq;
using ESchool.HomeAssignments.Domain;
using ESchool.HomeAssignments.Domain.Entities;
using ESchool.Libs.Application.Cqrs.Handlers;
using ESchool.Libs.Application.Cqrs.Query;
using ESchool.Libs.Domain.Services;

namespace ESchool.HomeAssignments.Application.Features.Homeworks
{
    public class HomeworkListAsTeacherQuery : PagedListQuery<HomeworkListAsTeacherResponse>
    {
        public Guid ClassId { get; set; }
        public Guid SubjectId { get; set; }
        public Guid SchoolYearId { get; set; }

        public bool IncludeReviewed { get; set; }
    }

    public class HomeworkListAsTeacherResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime Deadline { get; set; }
        public int Submissions { get; set; }
        public int Ratings { get; set; }
    }

    public class HomeworkListAsTeacherHandler : PagedListHandler<HomeworkListAsTeacherQuery, TeacherHomework, HomeworkListAsTeacherResponse>
    {
        private readonly Guid currentUserId;

        public HomeworkListAsTeacherHandler(HomeAssignmentsContext context, IIdentityService identityService) : base(context)
        {
            currentUserId = identityService.GetCurrentUserId();
        }

        protected override IQueryable<TeacherHomework> Filter(IQueryable<TeacherHomework> entities, HomeworkListAsTeacherQuery query)
        {
            return entities.Where(x => x.Homework.Lesson.ClassId == query.ClassId &&
                                       x.Homework.Lesson.SubjectId == query.SubjectId &&
                                       x.Homework.Lesson.SchoolYearId == query.SchoolYearId &&
                                       x.Teacher.UserId == currentUserId &&
                                       (query.IncludeReviewed || x.Homework.StudentHomeworks
                                           .Where(sh => sh.HomeworkSolution != null && sh.HomeworkSolution.TurnInDate != null)
                                           .All(sh => sh.HomeworkSolution.HomeworkReview != null)));
        }

        protected override IQueryable<HomeworkListAsTeacherResponse> Map(IQueryable<TeacherHomework> entities, HomeworkListAsTeacherQuery query)
        {
            return entities.Select(x => new HomeworkListAsTeacherResponse
            {
                Id = x.Homework.Id,
                Deadline = x.Homework.Deadline,
                Title = x.Homework.Title,
                Ratings = x.Homework.StudentHomeworks.Count(sh => sh.HomeworkSolution != null && sh.HomeworkSolution.HomeworkReview != null),
                Submissions = x.Homework.StudentHomeworks.Count(sh => sh.HomeworkSolution != null && sh.HomeworkSolution.TurnInDate != null)
            });
        }

        protected override IOrderedQueryable<TeacherHomework> Order(IQueryable<TeacherHomework> entities)
        {
            return entities.OrderBy(x => x.Homework.Deadline);
        }
    }
}