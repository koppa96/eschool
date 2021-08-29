using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Interface.Features.Grading.Grades;
using Flurl.Http;

namespace ESchool.Frontend.Network.ClassRegister.Endpoints.SchoolYears.ClassSchoolYears.ClassSchoolYearSubjects.ClassSchoolYearSubjectStudents
{
    public class ClassSchoolYearSubjectStudentGradesEndpoint
    {
        private readonly string baseUrl;
        private readonly IFlurlClient flurlClient;

        public ClassSchoolYearSubjectStudentGradesEndpoint(string baseUrl, IFlurlClient flurlClient)
        {
            this.baseUrl = baseUrl;
            this.flurlClient = flurlClient;
        }

        public Task<GradeDetailsResponse> CreateAsync(ClassSchoolYearSubjectGradeCreateDto dto,
            CancellationToken cancellationToken = default)
        {
            return flurlClient.Request(baseUrl)
                .PostJsonAsync(dto, cancellationToken)
                .ReceiveJson<GradeDetailsResponse>();
        }
    }
}