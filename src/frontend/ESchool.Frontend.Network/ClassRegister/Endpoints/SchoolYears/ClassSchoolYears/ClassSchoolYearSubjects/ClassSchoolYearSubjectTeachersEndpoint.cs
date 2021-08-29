using System.Threading;
using System.Threading.Tasks;
using Flurl.Http;

namespace ESchool.Frontend.Network.ClassRegister.Endpoints.SchoolYears.ClassSchoolYears.ClassSchoolYearSubjects
{
    public class ClassSchoolYearSubjectTeachersEndpoint
    {
        private readonly string baseUrl;
        private readonly IFlurlClient flurlClient;

        public ClassSchoolYearSubjectTeachersEndpoint(string baseUrl, IFlurlClient flurlClient)
        {
            this.baseUrl = baseUrl;
            this.flurlClient = flurlClient;
        }

        public Task AssignAsync(CancellationToken cancellationToken = default)
        {
            return flurlClient.Request(baseUrl)
                .PostAsync(cancellationToken: cancellationToken);
        }

        public Task RemoveAsync(CancellationToken cancellationToken = default)
        {
            return flurlClient.Request(baseUrl)
                .DeleteAsync(cancellationToken);
        }
    }
}