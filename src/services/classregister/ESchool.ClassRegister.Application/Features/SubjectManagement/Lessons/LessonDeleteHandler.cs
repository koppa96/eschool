using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities.SubjectManagement;
using ESchool.Libs.Application.Cqrs.Commands;
using ESchool.Libs.Application.Cqrs.Handlers;

namespace ESchool.ClassRegister.Application.Features.SubjectManagement.Lessons
{
    public class LessonDeleteCommand : DeleteCommand
    {
    }
    
    public class LessonDeleteHandler : DeleteHandler<LessonDeleteCommand, Lesson>
    {
        public LessonDeleteHandler(ClassRegisterContext context) : base(context)
        {
        }
    }
}