using System;
using MediatR;

namespace ESchool.ClassRegister.Interface.Features.SubjectManagement.Lessons
{
    public class LessonGetCommand : IRequest<LessonDetailsResponse>
    {
        public Guid Id { get; set; }
    }
}