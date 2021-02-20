using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities;
using ESchool.Libs.Application.Cqrs.Commands;
using ESchool.Libs.Application.Cqrs.Handlers;


namespace ESchool.ClassRegister.Application.Features.ClassTypes
{
    public class ClassTypeDeleteCommand : DeleteCommand
    {
    }
    
    public class ClassTypeDeleteHandler : DeleteHandler<ClassTypeDeleteCommand, ClassType>
    {
        public ClassTypeDeleteHandler(ClassRegisterContext context) : base(context)
        {
        }
    }
}