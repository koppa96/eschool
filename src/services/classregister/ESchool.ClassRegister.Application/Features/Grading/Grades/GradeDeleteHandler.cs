using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities.Grading;
using ESchool.Libs.Application.Cqrs.Handlers;
using ESchool.Libs.Interface.Commands;

namespace ESchool.ClassRegister.Application.Features.Grading.Grades
{
    public class GradeDeleteCommand : DeleteCommand
    {
    }
    
    public class GradeDeleteHandler : DeleteHandler<GradeDeleteCommand, Grade>
    {
        public GradeDeleteHandler(ClassRegisterContext context) : base(context)
        {
        }
    }
}