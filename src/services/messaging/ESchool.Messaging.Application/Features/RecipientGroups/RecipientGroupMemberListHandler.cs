using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESchool.Libs.Domain.Enums;
using ESchool.Messaging.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.Messaging.Application.Features.RecipientGroups
{
    public class RecipientGroupMemberListQuery : IRequest<List<MessagingUserListResponse>>
    {
        public Guid Id { get; set; }
    }

    public class MessagingUserListResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<TenantRoleType> Roles { get; set; }
    }

    public class RecipientGroupMemberListHandler : IRequestHandler<RecipientGroupMemberListQuery, List<MessagingUserListResponse>>
    {
        private readonly MessagingContext context;

        public RecipientGroupMemberListHandler(MessagingContext context)
        {
            this.context = context;
        }
        
        public async Task<List<MessagingUserListResponse>> Handle(RecipientGroupMemberListQuery request, CancellationToken cancellationToken)
        {
            var members = await context.RecipientGroupMembers.Where(x => x.RecipientGroupId == request.Id)
                .Select(x => new MessagingUserListResponse
                {
                    Id = x.MemberId,
                    Name = x.Member.Name,
                    Roles = x.Member.UserRoles.Select(x => x.TenantRoleType)
                        .ToList()
                })
                .ToListAsync(cancellationToken);
            return members;
        }
    }
}