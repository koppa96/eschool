using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Interface.Features.ClassTypes;
using ESchool.Libs.AspNetCore;
using ESchool.Libs.Interface.Commands;
using ESchool.Libs.Interface.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.ClassRegister.Api.Controllers
{
    [Authorize(PolicyNames.Administrator)]
    [Route("api/class-types")]
    public class ClassTypesController : ESchoolControllerBase
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