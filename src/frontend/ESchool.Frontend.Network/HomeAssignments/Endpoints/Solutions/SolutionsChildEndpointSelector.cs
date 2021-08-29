using ESchool.Frontend.Network.Abstractions;
using ESchool.Frontend.Network.HomeAssignments.Endpoints.Solutions.Files;

namespace ESchool.Frontend.Network.HomeAssignments.Endpoints.Solutions
{
    public class SolutionsChildEndpointSelector
    {
        public SolutionFilesEndpoint Files { get; }
        
        public SolutionsChildEndpointSelector(string basePath, ChildEndpointFactory childEndpointFactory)
        {
            Files = childEndpointFactory.CreateChildEndpoint<SolutionFilesEndpoint>(basePath + "/files");
        }
    }
}