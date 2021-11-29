using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Application.Features.SubjectManagement.Absences;
using ESchool.ClassRegister.Interface.Features.Grading.Grades;
using ESchool.Libs.Interface.Response;
using ESchool.Libs.Interface.Response.Common;
using Flurl.Http;

namespace ESchool.Frontend.Network.ClassRegister.Endpoints
{
    public class StudentsEndpoint
    {
        private readonly IFlurlClient flurlClient;
        
        public const string BasePath = ClassRegisterApi.BasePath + "/students";
        
        public StudentsEndpoint(IFlurlClient flurlClient)
        {
            this.flurlClient = flurlClient;
        }

        public Task<PagedListResponse<UserRoleListResponse>> ListUnassignedStudentsAsync(
            int pageIndex,
            int pageSize,
            string searchText = null,
            CancellationToken cancellationToken = default)
        {
            return flurlClient.Request(BasePath)
                .SetQueryParams(new { pageIndex, pageSize, searchText })
                .GetJsonAsync<PagedListResponse<UserRoleListResponse>>(cancellationToken);
        }

        public Task<PagedListResponse<AbsenceListResponse>> ListAbsencesAsync(
            Guid studentId,
            Guid schoolYearId,
            int pageIndex,
            int pageSize,
            CancellationToken cancellationToken = default)
        {
            return flurlClient.Request(BasePath, studentId, "absences")
                .SetQueryParams(new { schoolYearId, pageIndex, pageSize })
                .GetJsonAsync<PagedListResponse<AbsenceListResponse>>(cancellationToken);
        }

        public Task<List<GradeListByStudentResponse>> ListGradesAsync(
            Guid studentId,
            Guid schoolYearId,
            CancellationToken cancellationToken)
        {
            return flurlClient.Request(BasePath, studentId, "grades")
                .SetQueryParams(new { schoolYearId })
                .GetJsonAsync<List<GradeListByStudentResponse>>(cancellationToken);
        }
    }
}