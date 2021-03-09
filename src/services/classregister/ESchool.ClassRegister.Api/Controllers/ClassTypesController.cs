using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Application.Features.ClassTypes;
using ESchool.ClassRegister.Application.Features.ClassTypes.Common;
using ESchool.Libs.Application.Cqrs.Commands;
using ESchool.Libs.Application.Cqrs.Response;
using ESchool.Libs.AspNetCore;
using ESchool.Libs.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.ClassRegister.Api.Controllers
{
    [Authorize(PolicyNames.Administrator)]
    [ApiController]
    [Route("api/class-types")]
    public class ClassTypesController : ControllerBase
    {
        private readonly IMediator mediator;

        public ClassTypesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public Task<PagedListResponse<ClassTypeListResponse>> ListClassTypes([FromQuery] ClassTypeListQuery query,
            CancellationToken cancellationToken)
        {
            return mediator.Send(query, cancellationToken);
        }

        [HttpGet("{id}")]
        public Task<ClassTypeDetailsResponse> GetClassType(Guid id, CancellationToken cancellationToken)
        {
            return mediator.Send(new ClassTypeGetQuery { Id = id }, cancellationToken);
        }

        [HttpPost]
        public Task<ClassTypeDetailsResponse> CreateClassType([FromBody] ClassTypeCreateCommand command,
            CancellationToken cancellationToken)
        {
            return mediator.Send(command, cancellationToken);
        }

        [HttpPut("{id}")]
        public Task<ClassTypeDetailsResponse> EditClassType(Guid id, [FromBody] ClassTypeEditCommand command,
            CancellationToken cancellationToken)
        {
            return mediator.Send(new EditCommand<ClassTypeEditCommand, ClassTypeDetailsResponse>
            {
                Id = id,
                InnerCommand = command
            }, cancellationToken);
        }

        [HttpDelete("{id}")]
        public Task DeleteClassType(Guid id, CancellationToken cancellationToken)
        {
            return mediator.Send(new ClassTypeDeleteCommand { Id = id }, cancellationToken);
        }
        
    }
}