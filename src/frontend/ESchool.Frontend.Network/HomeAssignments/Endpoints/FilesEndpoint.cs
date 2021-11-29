using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Flurl.Http;

namespace ESchool.Frontend.Network.HomeAssignments.Endpoints
{
    public class FilesEndpoint
    {
        protected readonly string basePath;
        protected readonly IFlurlClient flurlClient;

        public FilesEndpoint(string basePath, IFlurlClient flurlClient)
        {
            this.basePath = basePath;
            this.flurlClient = flurlClient;
        }

        public Task<Stream> GetAsync(Guid fileId, CancellationToken cancellationToken = default)
        {
            return flurlClient.Request(basePath, fileId)
                .GetStreamAsync(cancellationToken);
        }

        public Task DeleteAsync(Guid fileId, CancellationToken cancellationToken = default)
        {
            return flurlClient.Request(basePath, fileId)
                .DeleteAsync(cancellationToken);
        }
    }
}