using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Application.Features.ClassSchoolYears;
using ESchool.ClassRegister.Interface.Features.Classes;
using ESchool.ClassRegister.Interface.Features.ClassSchoolYears;
using ESchool.Libs.AspNetCore;
using ESchool.Libs.Domain.Enums;
using ESchool.Libs.Interface.Query;
using ESchool.Libs.Interface.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.ClassRegister.Api.Controllers
{
    [Authorize(PolicyNames.Administrator)]
    [Route("api/school-years/{schoolYearId}/classes")]
    public class ClassSchoolYearsController : ESchoolControllerBase
    {
        private readonly IMediator mediator;

        public ClassSchoolYearsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("{classId}")]
        public Task AddClassToSchoolYear(Guid schoolYearId, Guid classId, CancellationToken cancellationToken)
        {
            return mediator.Send(new ClassSchoolYearCreateCommand
            {
                ClassId = classId,
                SchoolYearId = schoolYearId
            }, cancellationToken);
        }

        [HttpGet]
        public Task<PagedListResponse<ClassListResponse>> ListClassesInSchoolYear(Guid schoolYearId,
            [FromQuery] PagedListQuery query, CancellationToken cancellationToken)
        {
            var typedQuery = query.ToTypedQuery<ClassSchoolYearListQuery>(x => x.SchoolYearId = schoolYearId);
            return mediator.Send(typedQuery, cancellationToken);
        }

        [HttpDelete("{classId}")]
        public Task RemoveClassFromSchoolYear(Guid schoolYearId, Guid classId, CancellationToken cancellationToken)
        {
            return mediator.Send(new ClassSchoolYearDeleteCommand
            {
                ClassId = classId,
                SchoolYearId = schoolYearId
            }, cancellationToken);
        }
    }
}