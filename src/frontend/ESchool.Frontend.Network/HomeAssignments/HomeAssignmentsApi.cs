using ESchool.Frontend.Network.Abstractions;
using ESchool.Frontend.Network.HomeAssignments.Endpoints;
using ESchool.Frontend.Network.HomeAssignments.Endpoints.Homeworks;
using ESchool.Frontend.Network.HomeAssignments.Endpoints.Solutions;

namespace ESchool.Frontend.Network.HomeAssignments
{
    public class HomeAssignmentsApi
    {
        public const string BasePath = Api.BasePath + "/home-assignments";

        public FilesEndpoint Files { get; set; }
        public HomeworksEndpoint Homeworks { get; set; }
        public SolutionsEndpoint Solutions { get; set; }

        public HomeAssignmentsApi(ChildEndpointFactory childEndpointFactory)
        {
            Files = childEndpointFactory.CreateChildEndpoint<FilesEndpoint>(BasePath + "/files");
            Homeworks = childEndpointFactory.CreateChildEndpoint<HomeworksEndpoint>(BasePath + "/homeworks");
            Solutions = childEndpointFactory.CreateChildEndpoint<SolutionsEndpoint>(BasePath + "/solutions");
        }
    }
}