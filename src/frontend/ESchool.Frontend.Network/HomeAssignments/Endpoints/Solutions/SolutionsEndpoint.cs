using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.Frontend.Network.Abstractions;
using ESchool.HomeAssignments.Interface.Features.HomeworkReviews;
using ESchool.HomeAssignments.Interface.Features.HomeworkSolutions;
using ESchool.Libs.Interface.Response;
using Flurl.Http;

namespace ESchool.Frontend.Network.HomeAssignments.Endpoints.Solutions
{
    public class SolutionsEndpoint
    {
        protected readonly string basePath;
        protected readonly IFlurlClient flurlClient;
        protected readonly ChildEndpointFactory childEndpointFactory;

        public SolutionsChildEndpointSelector this[Guid solutionId] =>
            childEndpointFactory.CreateChildEndpointSelector<SolutionsChildEndpointSelector>(
                basePath + $"/{solutionId}");
        
        public SolutionsEndpoint(
            string basePath,
            IFlurlClient flurlClient,
            ChildEndpointFactory childEndpointFactory)
        {
            this.basePath = basePath;
            this.flurlClient = flurlClient;
            this.childEndpointFactory = childEndpointFactory;
        }

        public Task<HomeworkSolutionResponse> GetDetailsAsync(Guid solutionId,
            CancellationToken cancellationToken = default)
        {
            return flurlClient.Request(basePath, solutionId)
                .GetJsonAsync<HomeworkSolutionResponse>(cancellationToken);
        }

        public Task<HomeworkSolutionResponse> SubmitAsync(Guid solutionId,
            CancellationToken cancellationToken = default)
        {
            return flurlClient.Request(basePath, solutionId)
                .PatchAsync(cancellationToken: cancellationToken)
                .ReceiveJson<HomeworkSolutionResponse>();
        }

        public Task<HomeworkSolutionResponse> ReviewAsync(Guid solutionId, HomeworkReviewCreateCommand.Body commandBody,
            CancellationToken cancellationToken = default)
        {
            return flurlClient.Request(basePath, solutionId, "reviews")
                .PostJsonAsync(commandBody, cancellationToken)
                .ReceiveJson<HomeworkSolutionResponse>();
        }
    }
}