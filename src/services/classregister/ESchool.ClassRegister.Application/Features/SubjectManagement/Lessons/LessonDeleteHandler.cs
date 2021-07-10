using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities.SubjectManagement;
using ESchool.ClassRegister.Interface.Features.SubjectManagement.Lessons;
using ESchool.Libs.Application.Cqrs.Handlers;

namespace ESchool.ClassRegister.Application.Features.SubjectManagement.Lessons
{
    public class LessonDeleteHandler : DeleteHandler<LessonDeleteCommand, Lesson>
    {
        public LessonDeleteHandler(ClassRegisterContext context) : base(context)
        {
        }
    }
}