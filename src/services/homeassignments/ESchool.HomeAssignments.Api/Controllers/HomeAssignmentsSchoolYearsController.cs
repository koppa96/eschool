using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Grpc;
using ESchool.HomeAssignments.Application.Features.ClassSchoolYearSubjects;
using ESchool.HomeAssignments.Interface.Features;
using ESchool.HomeAssignments.Interface.Features.ClassSchoolYearSubjects;
using ESchool.Libs.AspNetCore;
using ESchool.Libs.Interface.Query;
using ESchool.Libs.Interface.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.HomeAssignments.Api.Controllers
{
    [Route("api/school-years")]
    public class HomeAssignmentsSchoolYearsController : ESchoolControllerBase 
    {
        private readonly IMediator mediator;

        public HomeAssignmentsSchoolYearsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Authorize(PolicyNames.Student)]
        [HttpGet("student")]
        public Task<List<ClassRegisterItemResponse>> ListSchoolYearsOfStudent(CancellationToken cancellationToken)
        {
            return mediator.Send(new StudentSchoolYearListQuery(), cancellationToken);
        }

        [Authorize(PolicyNames.Teacher)]
        [HttpGet("teacher")]
        public Task<List<ClassRegisterItemResponse>> ListSchoolYearsOfTeacher(CancellationToken cancellationToken)
        {
            return mediator.Send(new TeacherSchoolYearListQuery(), cancellationToken);
        }

        [Authorize(PolicyNames.Student)]
        [HttpGet("{schoolYearId}/subjects")]
        public Task<PagedListResponse<ClassRegisterItemResponse>> ListSubjects(Guid schoolYearId, [FromQuery] PagedListQuery query,
            CancellationToken cancellationToken)
        {
            return mediator.Send(query.ToTypedQuery<SubjectListQuery>(x =>
            {
                x.SchoolYearId = schoolYearId;
            }), cancellationToken);
        }
    }
}