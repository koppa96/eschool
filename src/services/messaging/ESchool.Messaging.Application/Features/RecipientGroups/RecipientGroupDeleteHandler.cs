using ESchool.Libs.Application.Cqrs.Handlers;
using ESchool.Messaging.Domain;
using ESchool.Messaging.Domain.Entities;
using ESchool.Messaging.Interface.RecipientGroups;

namespace ESchool.Messaging.Application.Features.RecipientGroups
{
    public class RecipientGroupDeleteHandler : DeleteHandler<RecipientGroupDeleteCommand, RecipientGroup>
    {
        public RecipientGroupDeleteHandler(MessagingContext context) : base(context)
        {
        }
    }
}