using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Interface.Features.SchoolYears;
using ESchool.Libs.AspNetCore;
using ESchool.Libs.Interface.Commands;
using ESchool.Libs.Interface.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.ClassRegister.Api.Controllers
{
    [Route("api/school-years")]
    public class SchoolYearsController : ESchoolControllerBase
    {
        private readonly IMediator mediator;

        public SchoolYearsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [Authorize(PolicyNames.AnyRole)]
        public Task<PagedListResponse<SchoolYearListResponse>> ListSchoolYears([FromQuery] SchoolYearListQuery query, CancellationToken cancellationToken)
        {
            return mediator.Send(query, cancellationToken);
        }

        [HttpGet("{id}")]
        [Authorize(PolicyNames.AnyRole)]
        public Task<SchoolYearDetailsResponse> GetSchoolYear(Guid id, CancellationToken cancellationToken)
        {
            return mediator.Send(new SchoolYearGetQuery { Id = id }, cancellationToken);
        }

        [HttpPost]
        [Authorize(PolicyNames.Administrator)]
        public Task<SchoolYearDetailsResponse> CreateSchoolYear([FromBody] SchoolYearCreateCommand command,
            CancellationToken cancellationToken)
        {
            return mediator.Send(command, cancellationToken);
        }

        [HttpPut("{id}")]
        [Authorize(PolicyNames.Administrator)]
        public Task<SchoolYearDetailsResponse> EditSchoolYear(Guid id, [FromBody] SchoolYearEditCommand command,
            CancellationToken cancellationToken)
        {
            return mediator.Send(new EditCommand<SchoolYearEditCommand, SchoolYearDetailsResponse>
            {
                Id = id,
                InnerCommand = command
            }, cancellationToken);
        }

        [HttpDelete("{id}")]
        [Authorize(PolicyNames.Administrator)]
        public Task DeleteSchoolYear(Guid id, CancellationToken cancellationToken)
        {
            return mediator.Send(new SchoolYearDeleteCommand { Id = id }, cancellationToken);
        }
    }
}