using System;
using MediatR;

namespace ESchool.Messaging.Interface.RecipientGroups
{
    public class RecipientGroupMemberAddCommand : IRequest
    {
        public Guid GroupId { get; set; }
        public Guid MemberId { get; set; }
    }
}