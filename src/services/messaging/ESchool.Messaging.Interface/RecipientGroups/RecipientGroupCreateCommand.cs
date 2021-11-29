using MediatR;

namespace ESchool.Messaging.Interface.RecipientGroups
{
    public class RecipientGroupCreateCommand : IRequest
    {
        public string Name { get; set; }
    }
}