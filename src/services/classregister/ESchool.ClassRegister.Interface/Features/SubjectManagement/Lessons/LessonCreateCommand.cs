using System;
using MediatR;

namespace ESchool.ClassRegister.Interface.Features.SubjectManagement.Lessons
{
    public class LessonCreateCommand : IRequest<LessonDetailsResponse>
    {
        public LessonCreateCommandBody Body { get; set; }
        public Guid SchoolYearId { get; set; }
        public Guid ClassId { get; set; }
        public Guid SubjectId { get; set; }
        

        public class LessonCreateCommandBody
        {
            public string Title { get; set; }
            public string Description { get; set; }

            public DateTime StartsAt { get; set; }
            public DateTime EndsAt { get; set; }
            public Guid ClassroomId { get; set; }
        }
    }
}