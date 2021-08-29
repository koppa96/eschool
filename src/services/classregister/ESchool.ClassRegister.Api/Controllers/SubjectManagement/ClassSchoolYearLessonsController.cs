using ESchool.ClassRegister.Interface.Features.SubjectManagement.Lessons;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ESchool.ClassRegister.Api.Controllers.SubjectManagement
{
    [Route("api/school-years/{schoolYearId}/classes/{classId}/lessons")]
    [ApiController]
    public class ClassSchoolYearLessonsController : ControllerBase
    {
        private readonly IMediator mediator;

        public ClassSchoolYearLessonsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public Task<List<LessonListResponse>> GetLessonsBetween(
            Guid schoolYearId,
            Guid classId,
            [FromQuery] DateTime from,
            [FromQuery] DateTime to,
            [FromQuery] bool showCanceled,
            CancellationToken cancellationToken)
        {
            return mediator.Send(new LessonListQuery
            {
                From = from,
                To = to,
                SchoolYearId = schoolYearId,
                ClassId = classId,
                ShowCanceled = showCanceled
            }, cancellationToken);
        }
    }
}
