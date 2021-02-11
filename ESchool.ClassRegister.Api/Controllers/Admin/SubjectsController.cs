using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Application.Features.Subjects;
using ESchool.ClassRegister.Application.Features.Subjects.Common;
using ESchool.Libs.Application.Cqrs.Response;
using ESchool.Libs.AspNetCore.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.ClassRegister.Api.Controllers.Admin
{
    [Route("api/admin/subjects")]
    public class SubjectsController : AdministratorControllerBase
    {
        private readonly IMediator mediator;

        public SubjectsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public Task<PagedListResponse<SubjectListResponse>> ListSubjects(CancellationToken cancellationToken)
        {
            return mediator.Send(new SubjectListQuery(), cancellationToken);
        }

        [HttpGet("{id}")]
        public Task<SubjectDetailsResponse> GetSubject(Guid id, CancellationToken cancellationToken)
        {
            return mediator.Send(new SubjectGetQuery { Id = id }, cancellationToken);
        }

        [HttpPost]
        public Task<SubjectDetailsResponse> CreateSubject([FromBody] SubjectCreateCommand command,
            CancellationToken cancellationToken)
        {
            return mediator.Send(command, cancellationToken);
        }

        [HttpPut("{id}")]
        public Task<SubjectDetailsResponse> EditSubject(Guid id, [FromBody] SubjectEditCommand command,
            CancellationToken cancellationToken)
        {
            return mediator.Send(command, cancellationToken);
        }

        [HttpDelete("{id}")]
        public Task DeleteSubject(Guid id, CancellationToken cancellationToken)
        {
            return mediator.Send(new SubjectDeleteCommand { Id = id }, cancellationToken);
        }
    }
}