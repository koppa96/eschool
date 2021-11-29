using System;
using MediatR;

namespace ESchool.ClassRegister.Interface.Features.SubjectManagement.Lessons
{
    public class LessonGetQuery : IRequest<LessonDetailsResponse>
    {
        public Guid Id { get; set; }
    }
}