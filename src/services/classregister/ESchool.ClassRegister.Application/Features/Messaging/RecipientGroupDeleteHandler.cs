using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities.Messaging;
using ESchool.Libs.Application.Cqrs.Handlers;
using ESchool.Libs.Interface.Commands;

namespace ESchool.ClassRegister.Application.Features.Messaging
{
    public class RecipientGroupDeleteCommand : DeleteCommand
    {
    }

    public class RecipientGroupDeleteHandler : DeleteHandler<RecipientGroupDeleteCommand, RecipientGroup>
    {
        public RecipientGroupDeleteHandler(ClassRegisterContext context) : base(context)
        {
        }
    }
}