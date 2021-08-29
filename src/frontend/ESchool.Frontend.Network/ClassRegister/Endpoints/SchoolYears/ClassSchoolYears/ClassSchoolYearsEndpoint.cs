using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.Frontend.Network.Abstractions;
using Flurl.Http;

namespace ESchool.Frontend.Network.ClassRegister.Endpoints.SchoolYears.ClassSchoolYears
{
    public class ClassSchoolYearsEndpoint
    {
        private readonly string basePath;
        private readonly IFlurlClient flurlClient;
        private readonly ChildEndpointFactory childEndpointFactory;

        public ClassSchoolYearsChildEndpointSelector this[Guid classId] =>
            childEndpointFactory.CreateChildEndpointSelector<ClassSchoolYearsChildEndpointSelector>(basePath + $"/{classId}");
        
        public ClassSchoolYearsEndpoint(
            string basePath,
            IFlurlClient flurlClient,
            ChildEndpointFactory childEndpointFactory)
        {
            this.basePath = basePath;
            this.flurlClient = flurlClient;
            this.childEndpointFactory = childEndpointFactory;
        }

        public Task AddAsync(Guid classId, CancellationToken cancellationToken = default)
        {
            return flurlClient.Request(basePath, classId)
                .PostAsync(cancellationToken: cancellationToken);
        }

        public Task RemoveAsync(Guid classId, CancellationToken cancellationToken = default)
        {
            return flurlClient.Request(basePath, classId)
                .DeleteAsync(cancellationToken);
        }
    }
}