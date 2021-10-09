using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Application.Features.Messaging;
using ESchool.ClassRegister.Application.Features.Users;
using ESchool.ClassRegister.Interface.Features.Users;
using ESchool.Libs.AspNetCore;
using ESchool.Libs.Interface.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.ClassRegister.Api.Controllers
{
    [Authorize(PolicyNames.AnyRole)]
    [Route("api/recipient-groups")]
    public class RecipientGroupsController : ESchoolControllerBase
    {
        private readonly IMediator mediator;

        public RecipientGroupsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public Task<PagedListResponse<RecipientGroupListResponse>> ListRecipientGroups(
            [FromQuery] RecipientGroupListQuery query, CancellationToken cancellationToken)
        {
            return mediator.Send(query, cancellationToken);
        }

        [HttpPost]
        public Task CreateRecipientGroup([FromBody] RecipientGroupCreateCommand command,
            CancellationToken cancellationToken)
        {
            return mediator.Send(command, cancellationToken);
        }

        [HttpDelete("{recipientGroupId}")]
        public Task DeleteRecipientGroup(Guid recipientGroupId, CancellationToken cancellationToken)
        {
            return mediator.Send(new RecipientGroupDeleteCommand
            {
                Id = recipientGroupId
            }, cancellationToken);
        }

        [HttpGet("{recipientGroupId}/members")]
        public Task<List<ClassRegisterUserListResponse>> ListRecipientGroupMembers(Guid recipientGroupId,
            CancellationToken cancellationToken)
        {
            return mediator.Send(new RecipientGroupMemberListQuery
            {
                Id = recipientGroupId
            }, cancellationToken);
        }

        [HttpPost("{recipientGroupId}/members/{userId}")]
        public Task AddMember(Guid recipientGroupId, Guid userId, CancellationToken cancellationToken)
        {
            return mediator.Send(new RecipientGroupMemberAddCommand
            {
                GroupId = recipientGroupId,
                MemberId = userId
            }, cancellationToken);
        }

        [HttpDelete("{recipientGroupId}/members/{userId}")]
        public Task RemoveMember(Guid recipientGroupId, Guid userId, CancellationToken cancellationToken)
        {
            return mediator.Send(new RecipientGroupMemberRemoveCommand
            {
                GroupId = recipientGroupId,
                MemberId = userId
            }, cancellationToken);
        }
    }
}