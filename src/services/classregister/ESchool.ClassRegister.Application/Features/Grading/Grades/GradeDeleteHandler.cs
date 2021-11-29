using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities.Grading;
using ESchool.ClassRegister.Interface.Features.Grading.Grades;
using ESchool.Libs.Application.Cqrs.Handlers;

namespace ESchool.ClassRegister.Application.Features.Grading.Grades
{
    public class GradeDeleteHandler : DeleteHandler<GradeDeleteCommand, Grade>
    {
        public GradeDeleteHandler(ClassRegisterContext context) : base(context)
        {
        }
    }
}