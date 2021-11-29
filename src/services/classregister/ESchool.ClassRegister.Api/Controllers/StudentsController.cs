using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Application.Features.Students;
using ESchool.ClassRegister.Application.Features.SubjectManagement.Absences;
using ESchool.ClassRegister.Application.Features.SubjectManagement.Lessons;
using ESchool.ClassRegister.Interface.Features.Grading.Grades;
using ESchool.ClassRegister.Interface.Features.SchoolYears;
using ESchool.ClassRegister.Interface.Features.Students;
using ESchool.ClassRegister.Interface.Features.SubjectManagement.Absences;
using ESchool.ClassRegister.Interface.Features.SubjectManagement.Lessons;
using ESchool.ClassRegister.Interface.Features.Subjects;
using ESchool.ClassRegister.Interface.Features.Users;
using ESchool.ClassRegister.Interface.Features.Users.Students;
using ESchool.Libs.AspNetCore;
using ESchool.Libs.Interface.Query;
using ESchool.Libs.Interface.Response;
using ESchool.Libs.Interface.Response.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.ClassRegister.Api.Controllers
{
    [Route("api/students")]
    public class StudentsController : ESchoolControllerBase
    {
        private readonly IMediator mediator;

        public StudentsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [Authorize(PolicyNames.Administrator)]
        public Task<PagedListResponse<UserRoleListResponse>> ListStudents([FromQuery] StudentListQuery query,
            CancellationToken cancellationToken)
        {
            return mediator.Send(query, cancellationToken);
        }

        [HttpGet("unassigned")]
        [Authorize(PolicyNames.Administrator)]
        public Task<PagedListResponse<UserRoleListResponse>> ListUnassignedStudents(
            [FromQuery] StudentsWithoutClassListQuery query, CancellationToken cancellationToken)
        {
            return mediator.Send(query, cancellationToken);
        }

        [HttpGet("{studentId}/absences")]
        [Authorize(PolicyNames.AnyRole)]
        public Task<PagedListResponse<AbsenceListResponse>> ListAbsences(Guid studentId,
            [FromQuery] Guid schoolYearId,
            [FromQuery] int pageIndex,
            [FromQuery] int pageSize,
            CancellationToken cancellationToken)
        {
            return mediator.Send(new AbsenceListQuery
            {
                StudentId = studentId,
                SchoolYearId = schoolYearId,
                PageIndex = pageIndex,
                PageSize = pageSize == 0 ? PagedListQuery.DefaultPageSize : pageSize
            }, cancellationToken);
        }

        [HttpGet("related")]
        [Authorize(PolicyNames.StudentOrParent)]
        public Task<List<UserRoleListResponse>> GetRelatedStudents(CancellationToken cancellationToken)
        {
            return mediator.Send(new RelatedStudentListQuery(), cancellationToken);
        }

        [HttpGet("{studentId}/school-years")]
        [Authorize(PolicyNames.StudentOrParent)]
        public Task<List<SchoolYearListResponse>> GetStudentSchoolYears(Guid studentId,
            CancellationToken cancellationToken)
        {
            return mediator.Send(new StudentSchoolYearListQuery
            {
                StudentId = studentId
            }, cancellationToken);
        }

        [HttpGet("{studentId}/school-years/{schoolYearId}/subjects")]
        [Authorize(PolicyNames.StudentOrParent)]
        public Task<PagedListResponse<SubjectListResponse>> GetStudentSubjectsInSchoolYear(Guid studentId,
            Guid schoolYearId, [FromQuery] PagedListQuery query, CancellationToken cancellationToken)
        {
            return mediator.Send(query.ToTypedQuery<StudentSubjectListQuery>(x =>
            {
                x.StudentId = studentId;
                x.SchoolYearId = schoolYearId;
            }), cancellationToken);
        }

        [HttpGet("{studentId}/school-years/{schoolYearId}/subjects/{subjectId}/grades")]
        [Authorize(PolicyNames.StudentOrParent)]
        public Task<PagedListResponse<GradeListResponse>> ListGrades(
            Guid studentId,
            Guid schoolYearId,
            Guid subjectId,
            [FromQuery] PagedListQuery query,
            CancellationToken cancellationToken)
        {
            return mediator.Send(query.ToTypedQuery<GradeListByStudentQuery>(x =>
            {
                x.StudentId = studentId;
                x.SchoolYearId = schoolYearId;
                x.SubjectId = subjectId;
            }), cancellationToken);
        }

        [HttpGet("{studentId}/school-years/{schoolYearId}/subjects/{subjectId}/lessons")]
        [Authorize(PolicyNames.StudentOrParent)]
        public Task<PagedListResponse<LessonListResponse>> ListLessons(
            Guid studentId,
            Guid schoolYearId,
            Guid subjectId,
            [FromQuery] PagedListQuery query,
            CancellationToken cancellationToken)
        {
            return mediator.Send(query.ToTypedQuery<StudentLessonListQuery>(x =>
            {
                x.StudentId = studentId;
                x.SchoolYearId = schoolYearId;
                x.SubjectId = subjectId;
            }), cancellationToken);
        }
    }
}