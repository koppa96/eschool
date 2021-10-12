using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.HomeAssignments.Application.Features.ClassSchoolYearSubjects;
using ESchool.Libs.AspNetCore;
using ESchool.Libs.Interface.Query;
using ESchool.Libs.Interface.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.HomeAssignments.Api.Controllers
{
    [Authorize(PolicyNames.Teacher)]
    [Route("api/school-years/{schoolYearId}/class-subjects")]
    public class HomeAssignmentsClassSubjectsController : ESchoolControllerBase
    {
        private readonly IMediator mediator;

        public HomeAssignmentsClassSubjectsController(IMediator mediator)
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