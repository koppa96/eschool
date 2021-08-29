using System;
using System.Threading;
using System.Threading.Tasks;
using Flurl.Http;

namespace ESchool.Frontend.Network.ClassRegister.Endpoints.Subjects
{
    public class SubjectTeachersEndpoint
    {
        private readonly string basePath;
        private readonly IFlurlClient flurlClient;

        public SubjectTeachersEndpoint(string basePath, IFlurlClient flurlClient)
        {
            this.basePath = basePath;
            this.flurlClient = flurlClient;
        }
        
        public Task AssignAsync(Guid teacherId, CancellationToken cancellationToken = default)
        {
            return flurlClient.Request(basePath, teacherId)
                .PostAsync(cancellationToken: cancellationToken);
        }

        public Task RemoveAsync(Guid teacherId, CancellationToken cancellationToken = default)
        {
            return flurlClient.Request(basePath, teacherId)
                .DeleteAsync(cancellationToken);
        }
    }
}