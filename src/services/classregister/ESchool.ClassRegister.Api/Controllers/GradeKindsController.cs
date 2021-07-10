using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Interface.Features.Grading.GradeKinds;
using ESchool.Libs.AspNetCore;
using ESchool.Libs.Interface.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.ClassRegister.Api.Controllers
{
    [Authorize(PolicyNames.Administrator)]
    [ApiController]
    [Route("api/grade-kinds")]
    public class GradeKindsController : ControllerBase
    {
        private readonly IMediator mediator;

        public GradeKindsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public Task<List<GradeKindResponse>> ListGradeKinds(CancellationToken cancellationToken)
        {
            return mediator.Send(new GradeKindListQuery(), cancellationToken);
        }

        [HttpPost]
        public Task<GradeKindResponse> CreateGradeKind(
            [FromBody] GradeKindCreateCommand command,
            CancellationToken cancellationToken)
        {
            return mediator.Send(command, cancellationToken);
        }

        [HttpPut("{gradeKindId}")]
        public Task<GradeKindResponse> EditGradeKind(
            Guid gradeKindId,
            [FromBody] GradeKindEditCommand command,
            CancellationToken cancellationToken)
        {
            return mediator.Send(new EditCommand<GradeKindEditCommand, GradeKindResponse>
            {
                Id = gradeKindId,
                InnerCommand = command
            }, cancellationToken);
        }

        [HttpDelete("{gradeKindId}")]
        public Task DeleteGradeKind(Guid gradeKindId, CancellationToken cancellationToken)
        {
            return mediator.Send(new GradeKindDeleteCommand
            {
                Id = gradeKindId
            }, cancellationToken);
        }
    }
}