using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities;
using ESchool.ClassRegister.Interface.Features.ClassTypes;
using ESchool.Libs.Application.Cqrs.Handlers;

namespace ESchool.ClassRegister.Application.Features.ClassTypes
{
    public class ClassTypeDeleteHandler : DeleteHandler<ClassTypeDeleteCommand, ClassType>
    {
        public ClassTypeDeleteHandler(ClassRegisterContext context) : base(context)
        {
        }
    }
}