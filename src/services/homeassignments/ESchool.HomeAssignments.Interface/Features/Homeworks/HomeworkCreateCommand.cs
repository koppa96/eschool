using System;
using MediatR;

namespace ESchool.HomeAssignments.Interface.Features.Homeworks
{
    public class HomeworkCreateCommand : IRequest<HomeworkDetailsResponse>
    {
        public Guid LessonId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Optional { get; set; }
        public DateTime Deadline { get; set; }
    }
}