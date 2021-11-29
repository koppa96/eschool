using ESchool.ClassRegister.Interface.Features.SubjectManagement.Lessons;
using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ESchool.Frontend.Network.ClassRegister.Endpoints.SchoolYears.ClassSchoolYears
{
    public class ClassSchoolYearLessonsEndpoint
    {
        private readonly string baseUrl;
        private readonly IFlurlClient flurlClient;

        public ClassSchoolYearLessonsEndpoint(string baseUrl, IFlurlClient flurlClient)
        {
            this.baseUrl = baseUrl;
            this.flurlClient = flurlClient;
        }

        public Task<List<LessonListResponse>> GetAllListAsync(DateTime from, DateTime to, bool showCanceled = false, CancellationToken cancellationToken = default)
        {
            return flurlClient.Request(baseUrl)
                .SetQueryParams(new { from, to, showCanceled })
                .GetJsonAsync<List<LessonListResponse>>(cancellationToken);
        }
    }
}
