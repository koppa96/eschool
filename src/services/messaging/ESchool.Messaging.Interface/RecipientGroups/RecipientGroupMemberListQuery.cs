using System;
using System.Collections.Generic;
using MediatR;

namespace ESchool.Messaging.Interface.RecipientGroups
{
    public class RecipientGroupMemberListQuery : IRequest<List<MessagingUserListResponse>>
    {
        public Guid Id { get; set; }
    }
}