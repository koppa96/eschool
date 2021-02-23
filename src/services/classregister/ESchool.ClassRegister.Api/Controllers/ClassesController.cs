using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Application.Features.Classes;
using ESchool.ClassRegister.Application.Features.Classes.Common;
using ESchool.Libs.Application.Cqrs.Commands;
using ESchool.Libs.Application.Cqrs.Query;
using ESchool.Libs.Application.Cqrs.Response;
using ESchool.Libs.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.ClassRegister.Api.Controllers
{
    [Authorize(nameof(TenantRoleType.Administrator))]
    [Route("api/classes")]
    public class ClassesController : ControllerBase
    {
        private readonly IMediator mediator;

        public ClassesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public Task<PagedListResponse<ClassListResponse>> ListClasses(
            [FromQuery] PagedListQuery<ClassListResponse> query, CancellationToken cancellationToken)
        {
            return mediator.Send(query, cancellationToken);
        }

        [HttpGet("{id}")]
        public Task<ClassDetailsResponse> GetClass(Guid id, CancellationToken cancellationToken)
        {
            return mediator.Send(new ClassGetQuery
            {
                Id = id
            }, cancellationToken);
        }

        [HttpPost]
        public Task<ClassDetailsResponse> CreateClass([FromBody] ClassCreateCommand command,
            CancellationToken cancellationToken)
        {
            return mediator.Send(command, cancellationToken);
        }

        [HttpPut("{id}")]
        public Task<ClassDetailsResponse> EditClass(Guid id, [FromBody] ClassEditCommand command,
            CancellationToken cancellationToken)
        {
            return mediator.Send(new EditCommand<ClassEditCommand, ClassDetailsResponse>
            {
                Id = id,
                InnerCommand = command
            }, cancellationToken);
        }

        [HttpDelete("{id}")]
        public Task DeleteClass(Guid id, CancellationToken cancellationToken)
        {
            return mediator.Send(new ClassDeleteCommand
            {
                Id = id
            }, cancellationToken);
        }
    }
}