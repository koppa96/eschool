using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Application.Features.ClassSchoolYears;
using ESchool.Libs.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.ClassRegister.Api.Controllers
{
    [Authorize(nameof(TenantRoleType.Administrator))]
    [ApiController]
    [Route("api/school-years/{schoolYearId}/classes")]
    public class ClassSchoolYearsController : ControllerBase
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