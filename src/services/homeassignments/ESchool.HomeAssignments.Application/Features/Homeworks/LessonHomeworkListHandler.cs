using System.Linq;
using ESchool.HomeAssignments.Domain;
using ESchool.HomeAssignments.Domain.Entities;
using ESchool.HomeAssignments.Interface.Features.Homeworks;
using ESchool.HomeAssignments.Interface.Features.UserHomeworks;
using ESchool.Libs.Application.Cqrs.Handlers;

namespace ESchool.HomeAssignments.Application.Features.Homeworks
{
    public class LessonHomeworkListHandler : PagedListHandler<LessonHomeworkListQuery, Homework, TeacherHomeworkListResponse>
    {

        public LessonHomeworkListHandler(HomeAssignmentsContext context) : base(context)
        {
        }

        protected override IQueryable<Homework> Filter(IQueryable<Homework> entities, LessonHomeworkListQuery query)
        {
            return entities.Where(x => x.Lesson.Id == query.LessonId);
        }

        protected override IQueryable<TeacherHomeworkListResponse> Map(IQueryable<Homework> entities, LessonHomeworkListQuery query)
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

        protected override IOrderedQueryable<Homework> Order(IQueryable<Homework> entities, LessonHomeworkListQuery query)
        {
            return entities.OrderByDescending(x => x.Deadline);
        }
    }
}