using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Application.Features.Parents;
using ESchool.Libs.AspNetCore;
using ESchool.Libs.Interface.Query;
using ESchool.Libs.Interface.Response;
using ESchool.Libs.Interface.Response.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.ClassRegister.Api.Controllers
{
    [Route("api/parents")]
    public class ParentsController : ESchoolControllerBase
    {
        private readonly IMediator mediator;

        public ParentsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public Task<PagedListResponse<UserRoleListResponse>> ListParents([FromQuery] PagedListQuery query,
            CancellationToken cancellationToken)
        {
            return mediator.Send(query.ToTypedQuery<ParentListQuery>(), cancellationToken);
        }

        [HttpGet("{parentId}/students")]
        public Task<List<UserRoleListResponse>> ListStudentsOfParent(Guid parentId, CancellationToken cancellationToken)
        {
            return mediator.Send(new ParentStudentListQuery
            {
                ParentId = parentId
            }, cancellationToken);
        }

        [HttpPost("{parentId}/students/{studentId}")]
        public Task AssignParentToStudent(Guid parentId, Guid studentId, CancellationToken cancellationToken)
        {
            return mediator.Send(new AssignParentToStudentCommand
            {
                ParentId = parentId,
                StudentId = studentId
            }, cancellationToken);
        }

        [HttpDelete("{parentId}/students/{studentId}")]
        public Task RemoveParentFromStudent(Guid parentId, Guid studentId, CancellationToken cancellationToken)
        {
            return mediator.Send(new RemoveParentFromStudentCommand
            {
                ParentId = parentId,
                StudentId = studentId
            }, cancellationToken);
        }
    }
}