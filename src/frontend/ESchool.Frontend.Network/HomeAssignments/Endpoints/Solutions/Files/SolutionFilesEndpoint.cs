using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ESchool.HomeAssignments.Interface.Features.HomeworkSolutions;
using Flurl.Http;

namespace ESchool.Frontend.Network.HomeAssignments.Endpoints.Solutions.Files
{
    public class SolutionFilesEndpoint : FilesEndpoint
    {
        public SolutionFilesEndpoint(string basePath, IFlurlClient flurlClient) : base(basePath, flurlClient)
        {
        }

        public Task<HomeworkSolutionResponse> CreateAsync(string name, Stream stream, CancellationToken cancellationToken = default)
        {
            return flurlClient.Request(basePath)
                .PostMultipartAsync(content => content.AddFile(name, stream, name), cancellationToken)
                .ReceiveJson<HomeworkSolutionResponse>();
        }
    }
}