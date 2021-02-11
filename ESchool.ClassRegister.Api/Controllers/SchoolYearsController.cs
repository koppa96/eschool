using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Application.Features.SchoolYears;
using ESchool.ClassRegister.Application.Features.SchoolYears.Common;
using ESchool.Libs.Application.Cqrs.Response;
using ESchool.Libs.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.ClassRegister.Api.Controllers
{
    [Authorize(nameof(TenantRoleType.Administrator))]
    [Route("api/school-years")]
    public class SchoolYearsController : ControllerBase
    {
        private readonly IMediator mediator;

        public SchoolYearsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public Task<PagedListResponse<SchoolYearListResponse>> ListSchoolYears([FromQuery] SchoolYearListQuery query, CancellationToken cancellationToken)
        {
            return mediator.Send(query, cancellationToken);
        }

        [HttpGet("{id}")]
        public Task<SchoolYearDetailsResponse> GetSchoolYear(Guid id, CancellationToken cancellationToken)
        {
            return mediator.Send(new SchoolYearGetQuery { Id = id }, cancellationToken);
        }

        [HttpPost]
        public Task<SchoolYearDetailsResponse> CreateSchoolYear([FromBody] SchoolYearCreateCommand command,
            CancellationToken cancellationToken)
        {
            return mediator.Send(command, cancellationToken);
        }

        [HttpPut("{id}")]
        public Task<SchoolYearDetailsResponse> EditSchoolYear(Guid id, [FromBody] SchoolYearEditCommand command,
            CancellationToken cancellationToken)
        {
            return mediator.Send(command, cancellationToken);
        }

        [HttpDelete("{id}")]
        public Task DeleteSchoolYear(Guid id, CancellationToken cancellationToken)
        {
            return mediator.Send(new SchoolYearDeleteCommand { Id = id }, cancellationToken);
        }
    }
}