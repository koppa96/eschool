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
    [Route("api/grade-kinds")]
    public class GradeKindsController : ESchoolControllerBase
    {
        private readonly IMediator mediator;

        public GradeKindsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Authorize(PolicyNames.AnyRole)]
        [HttpGet]
        public Task<List<GradeKindResponse>> ListGradeKinds(CancellationToken cancellationToken)
        {
            return mediator.Send(new GradeKindListQuery(), cancellationToken);
        }

        [Authorize(PolicyNames.Administrator)]
        [HttpPost]
        public Task<GradeKindResponse> CreateGradeKind(
            [FromBody] GradeKindCreateCommand command,
            CancellationToken cancellationToken)
        {
            return mediator.Send(command, cancellationToken);
        }

        [Authorize(PolicyNames.Administrator)]
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

        [Authorize(PolicyNames.Administrator)]
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