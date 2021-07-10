using System;
using System.Collections.Generic;
using MediatR;

namespace ESchool.ClassRegister.Interface.Features.SubjectManagement.Lessons
{
    public class LessonListQuery : IRequest<List<LessonListResponse>>
    {
        public Guid ClassId { get; set; }
        public Guid SchoolYearId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public bool ShowCanceled { get; set; }
    }
}