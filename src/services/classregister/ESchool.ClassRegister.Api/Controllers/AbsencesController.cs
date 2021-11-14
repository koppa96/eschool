using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Application.Features.SubjectManagement.Absences;
using ESchool.ClassRegister.Interface.Features.SubjectManagement.Absences;
using ESchool.Libs.AspNetCore;
using ESchool.Libs.Interface.Commands;
using ESchool.Libs.Interface.Query;
using ESchool.Libs.Interface.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.ClassRegister.Api.Controllers
{
    [Route("api/absences")]
    [Route("api/school-years/{schoolYearId}/classes/{classId}/subjects/{subjectId}/lessons/{lessonId}/absences")]
    public class AbsencesController : ESchoolControllerBase
    {
        private readonly IMediator mediator;

        public AbsencesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [Authorize(PolicyNames.Teacher)]
        public Task<PagedListResponse<LessonAbsenceListResponse>> ListLessonAbsences(Guid lessonId,
            [FromQuery] PagedListQuery query, CancellationToken cancellationToken)
        {
            return mediator.Send(query.ToTypedQuery<LessonAbsenceListQuery>(x =>
            {
                x.LessonId = lessonId;
            }), cancellationToken);
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