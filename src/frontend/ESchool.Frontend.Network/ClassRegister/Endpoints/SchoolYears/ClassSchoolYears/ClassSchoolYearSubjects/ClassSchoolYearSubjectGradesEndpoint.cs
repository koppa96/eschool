using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Interface.Features.Grading.Grades;
using Flurl.Http;

namespace ESchool.Frontend.Network.ClassRegister.Endpoints.SchoolYears.ClassSchoolYears.ClassSchoolYearSubjects
{
    public class ClassSchoolYearSubjectGradesEndpoint
    {
        private readonly string basePath;
        private readonly IFlurlClient flurlClient;

        public ClassSchoolYearSubjectGradesEndpoint(string basePath, IFlurlClient flurlClient)
        {
            this.basePath = basePath;
            this.flurlClient = flurlClient;
        }

        public Task<List<GradeListByClassSchoolYearSubjectResponse>> GetAllListAsync(
            CancellationToken cancellationToken = default)
        {
            return flurlClient.Request(basePath)
                .GetJsonAsync<List<GradeListByClassSchoolYearSubjectResponse>>(cancellationToken);
        }
    }
}