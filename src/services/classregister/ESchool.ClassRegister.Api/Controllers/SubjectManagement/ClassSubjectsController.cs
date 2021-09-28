using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Application.Features.SubjectManagement.ClassSubjects;
using ESchool.Libs.AspNetCore;
using ESchool.Libs.Interface.Query;
using ESchool.Libs.Interface.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.ClassRegister.Api.Controllers.SubjectManagement
{
    [Authorize(PolicyNames.Teacher)]
    [Route("api/school-years/{schoolYearId}/class-subjects")]
    [ApiController]
    public class ClassSubjectsController : ControllerBase
    {
        private readonly IMediator mediator;

        public ClassSubjectsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public Task<PagedListResponse<ClassSubjectListResponse>> ListClassSubjects(Guid schoolYearId,
            [FromQuery] PagedListQuery query, CancellationToken cancellationToken)
        {
            return mediator.Send(query.ToTypedQuery<ClassSubjectListQuery>(x =>
            {
                x.SchoolYearId = schoolYearId;
            }), cancellationToken);
        }
    }
}