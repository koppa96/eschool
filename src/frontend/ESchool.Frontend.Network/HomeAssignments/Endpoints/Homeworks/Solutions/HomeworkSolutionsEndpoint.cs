using System.Threading;
using System.Threading.Tasks;
using ESchool.Frontend.Network.Abstractions;
using ESchool.Frontend.Network.HomeAssignments.Endpoints.Solutions;
using ESchool.HomeAssignments.Interface.Features.HomeworkSolutions;
using ESchool.Libs.Interface.Response;
using Flurl.Http;

namespace ESchool.Frontend.Network.HomeAssignments.Endpoints.Homeworks.Solutions
{
    public class HomeworkSolutionsEndpoint : SolutionsEndpoint
    {
        public HomeworkSolutionsEndpoint(
            string basePath,
            IFlurlClient flurlClient,
            ChildEndpointFactory childEndpointFactory) : base(basePath, flurlClient, childEndpointFactory)
        {
        }

        public Task<PagedListResponse<HomeworkSolutionListResponse>> GetPagedListAsync(
            int pageIndex,
            int pageSize,
            CancellationToken cancellationToken = default)
        {
            return flurlClient.Request(basePath)
                .SetQueryParams(new { pageIndex, pageSize })
                .GetJsonAsync<PagedListResponse<HomeworkSolutionListResponse>>(cancellationToken);
        }
    }
}