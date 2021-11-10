using ESchool.Libs.Application.Cqrs.Handlers;
using ESchool.Libs.Interface.Commands;
using ESchool.Messaging.Domain;
using ESchool.Messaging.Domain.Entities;

namespace ESchool.Messaging.Application.Features.RecipientGroups
{
    public class RecipientGroupDeleteCommand : DeleteCommand
    {
    }

    public class RecipientGroupDeleteHandler : DeleteHandler<RecipientGroupDeleteCommand, RecipientGroup>
    {
        public RecipientGroupDeleteHandler(MessagingContext context) : base(context)
        {
        }
    }
}