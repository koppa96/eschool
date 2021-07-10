using System;
using MediatR;

namespace ESchool.ClassRegister.Interface.Features.SubjectManagement.Lessons
{
    public class LessonInfoQuery : IRequest<LessonInfoResponse>
    {
        public Guid Id { get; set; }
    }
}