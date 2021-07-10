using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Application.Features.SubjectManagement.Absences;
using ESchool.Libs.AspNetCore;
using ESchool.Libs.Interface.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.ClassRegister.Api.Controllers
{
    [ApiController]
    [Route("api/absences")]
    [Route("api/school-years/{schoolYearId}/classes/{classId}/subjects/{subjectId}/lessons/{lessonId}/absences")]
    public class AbsencesController : ControllerBase
    {
        private readonly IMediator mediator;

        public AbsencesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPatch("{absenceId}")]
        [Authorize(PolicyNames.TeacherOrAdministrator)]
        public Task SetAbsenceState(Guid absenceId,
            [FromBody] AbsenceStateSetCommand command,
            CancellationToken cancellationToken)
        {
            return mediator.Send(new EditCommand<AbsenceStateSetCommand>
            {
                Id = absenceId,
                InnerCommand = command
            }, cancellationToken);
        }

        [HttpDelete("{absenceId}")]
        [Authorize(PolicyNames.TeacherOrAdministrator)]
        public Task DeleteAbsence(Guid absenceId, CancellationToken cancellationToken)
        {
            return mediator.Send(new AbsenceDeleteCommand
            {
                Id = absenceId
            }, cancellationToken);
        }
    }
}