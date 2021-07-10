using System.Linq;
using ESchool.HomeAssignments.Domain;
using ESchool.HomeAssignments.Domain.Entities;
using ESchool.HomeAssignments.Interface.Features.UserHomeworks;
using ESchool.Libs.Application.Cqrs.Handlers;
using ESchool.Libs.Domain.Services;

namespace ESchool.HomeAssignments.Application.Features.UserHomeworks
{
    public class TeacherHomeworkListHandler : PagedListHandler<TeacherHomeworkListQuery, Homework, TeacherHomeworkListResponse>
    {
        private readonly IIdentityService identityService;

        public TeacherHomeworkListHandler(HomeAssignmentsContext context, IIdentityService identityService) : base(context)
        {
            this.identityService = identityService;
        }

        protected override IQueryable<Homework> Filter(IQueryable<Homework> entities, TeacherHomeworkListQuery query)
        {
            var currentUserId = identityService.GetCurrentUserId();
            return entities.Where(x =>
                x.Lesson.ClassSchoolYearSubject.ClassSchoolYearSubjectTeachers.Any(t =>
                    t.Teacher.UserId == currentUserId));
        }

        protected override IQueryable<TeacherHomeworkListResponse> Map(IQueryable<Homework> entities, TeacherHomeworkListQuery query)
        {
            return entities.Select(x => new TeacherHomeworkListResponse
            {
                Id = x.Id,
                Title = x.Title,
                Deadline = x.Deadline,
                Optional = x.Optional,
                Submissions = x.Solutions.Count(s => s.TurnInDate != null),
                Reviews = x.Solutions.Count(s => s.HomeworkReview != null)
            });
        }

        protected override IOrderedQueryable<Homework> Order(IQueryable<Homework> entities)
        {
            return entities.OrderByDescending(x => x.Deadline);
        }
    }
}