using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Application.Features.Grading.Grades;
using ESchool.ClassRegister.Interface.Features.Grading.Grades;
using ESchool.Libs.AspNetCore;
using ESchool.Libs.Interface.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.ClassRegister.Api.Controllers
{
    
    [Route("api/grades")]
    [ApiController]
    public class GradesController : ControllerBase
    {
        private readonly IMediator mediator;

        public GradesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Authorize(PolicyNames.AnyRole)]
        [HttpGet("{gradeId}")]
        public Task<GradeDetailsResponse> GetGradeDetails(Guid gradeId, CancellationToken cancellationToken)
        {
            var query = new GradeGetQuery
            {
                Id = gradeId
            };

            return mediator.Send(query, cancellationToken);
        }

        [Authorize(PolicyNames.Teacher)]
        [HttpPut("{gradeId}")]
        public Task<GradeDetailsResponse> EditGrade(Guid gradeId, [FromBody] GradeEditCommand commandBody,
            CancellationToken cancellationToken)
        {
            var command = new EditCommand<GradeEditCommand, GradeDetailsResponse>
            {
                Id = gradeId,
                InnerCommand = commandBody
            };

            return mediator.Send(command, cancellationToken);
        }

        [Authorize(PolicyNames.Teacher)]
        [HttpDelete("{gradeId}")]
        public Task DeleteGrade(Guid gradeId, CancellationToken cancellationToken)
        {
            var command = new GradeDeleteCommand
            {
                Id = gradeId
            };

            return mediator.Send(command, cancellationToken);
        }
    }
}