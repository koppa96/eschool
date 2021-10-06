using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Interface.Features.SubjectManagement.ClassSchoolYearSubjects;
using ESchool.ClassRegister.Interface.Features.Subjects;
using ESchool.Libs.AspNetCore;
using ESchool.Libs.Interface.Query;
using ESchool.Libs.Interface.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.ClassRegister.Api.Controllers.SubjectManagement
{
    [Authorize(PolicyNames.Administrator)]
    [Route("api/school-years/{schoolYearId}/classes/{classId}/subjects")]
    public class ClassSchoolYearSubjectsController : ESchoolControllerBase
    {
        private readonly IMediator mediator;

        public ClassSchoolYearSubjectsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public Task<PagedListResponse<SubjectListResponse>> ListClassSchoolYearSubjects(Guid schoolYearId,
            Guid classId,
            [FromQuery] int pageIndex,
            [FromQuery] int pageSize,
            CancellationToken cancellationToken)
        {

            return mediator.Send(new ClassSchoolYearSubjectListQuery
            {
                SchoolYearId = schoolYearId,
                ClassId = classId,
                PageIndex = pageIndex,
                PageSize = pageSize == 0 ? PagedListQuery.DefaultPageSize : pageSize
            }, cancellationToken);
        }

        [HttpGet("{subjectId}")]
        public Task<ClassSchoolYearSubjectDetailsResponse> GetDetails(Guid schoolYearId,
            Guid classId,
            Guid subjectId,
            CancellationToken cancellationToken)
        {
            return mediator.Send(new ClassSchoolYearSubjectQuery
            {
                ClassId = classId,
                SubjectId = subjectId,
                SchoolYearId = schoolYearId
            }, cancellationToken);
        }

        [HttpPost("{subjectId}")]
        public Task CreateClassSchoolYearSubject(Guid schoolYearId,
            Guid classId,
            Guid subjectId,
            [FromBody] List<Guid> teacherIds,
            CancellationToken cancellationToken)
        {
            return mediator.Send(new ClassSchoolYearSubjectCreateCommand
            {
                ClassId = classId,
                SchoolYearId = schoolYearId,
                SubjectId = subjectId,
                TeacherIds = teacherIds
            }, cancellationToken);
        }

        [HttpPut("{subjectId}")]
        public Task EditClassSchoolYearSubject(Guid schoolYearId,
            Guid classId,
            Guid subjectId,
            [FromBody] List<Guid> teacherIds,
            CancellationToken cancellationToken)
        {
            return mediator.Send(new ClassSchoolYearSubjectEditCommand
            {
                ClassId = classId,
                SchoolYearId = schoolYearId,
                SubjectId = subjectId,
                TeacherIds = teacherIds
            }, cancellationToken);
        }

        [HttpDelete("{subjectId}")]
        public Task DeleteClassSchoolYearSubject(Guid schoolYearId,
            Guid classId,
            Guid subjectId,
            CancellationToken cancellationToken)
        {
            return mediator.Send(new ClassSchoolYearSubjectDeleteCommand
            {
                ClassId = classId,
                SchoolYearId = schoolYearId,
                SubjectId = subjectId
            }, cancellationToken);
        }
    }
}