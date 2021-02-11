using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Application.Features.Classrooms;
using ESchool.ClassRegister.Application.Features.Classrooms.Common;
using ESchool.Libs.Application.Cqrs.Response;
using ESchool.Libs.AspNetCore.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.ClassRegister.Api.Controllers.Admin
{
    [Route("api/admin/classrooms")]
    public class ClassroomsController : AdministratorControllerBase
    {
        private readonly IMediator mediator;

        public ClassroomsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public Task<PagedListResponse<ClassroomListResponse>> ListClassrooms(CancellationToken cancellationToken)
        {
            return mediator.Send(new ClassroomListQuery(), cancellationToken);
        }

        [HttpGet("{id}")]
        public Task<ClassroomDetailsResponse> GetClassroom(Guid id, CancellationToken cancellationToken)
        {
            return mediator.Send(new ClassroomGetQuery { Id = id }, cancellationToken);
        }

        [HttpPost]
        public Task<ClassroomDetailsResponse> CreateClassroom([FromBody] ClassroomCreateCommand command,
            CancellationToken cancellationToken)
        {
            return mediator.Send(command, cancellationToken);
        }

        [HttpPut("{id}")]
        public Task<ClassroomDetailsResponse> EditClassroom([FromBody] ClassroomEditCommand command,
            CancellationToken cancellationToken)
        {
            return mediator.Send(command, cancellationToken);
        }

        [HttpDelete("{id}")]
        public Task DeleteClassroom(Guid id, CancellationToken cancellationToken)
        {
            return mediator.Send(new ClassroomDeleteCommand { Id = id }, cancellationToken);
        }
    }
}