using ESchool.Frontend.Network.Abstractions;
using ESchool.Frontend.Network.HomeAssignments.Endpoints.Homeworks.HomeworkSolutions;

namespace ESchool.Frontend.Network.HomeAssignments.Endpoints.Homeworks
{
    public class HomeworksChildEndpointSelector
    {
        public HomeworkSolutionsEndpoint Solutions { get; }
        
        public HomeworksChildEndpointSelector(string basePath, ChildEndpointFactory childEndpointFactory)
        {
            Solutions = childEndpointFactory
                .CreateChildEndpoint<HomeworkSolutionsEndpoint>(basePath + "/solutions");
        }
    }
}