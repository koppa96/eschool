using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.Frontend.Network.Abstractions;
using ESchool.Testing.Interface.Features.TestAnswers;
using Flurl.Http;

namespace ESchool.Frontend.Network.Testing.Answers
{
    public class AnswersEndpoint
    {
        protected readonly string basePath;
        protected readonly IFlurlClient flurlClient;
        protected readonly ChildEndpointFactory childEndpointFactory;

        public AnswersChildEndpointSelector this[Guid answerId] =>
            childEndpointFactory.CreateChildEndpointSelector<AnswersChildEndpointSelector>(basePath + $"/{answerId}");
        
        public AnswersEndpoint(string basePath, IFlurlClient flurlClient, ChildEndpointFactory childEndpointFactory)
        {
            this.basePath = basePath;
            this.flurlClient = flurlClient;
            this.childEndpointFactory = childEndpointFactory;
        }

        public Task<TestAnswerDetailsResponse> GetDetailsAsync(Guid answerId,
            CancellationToken cancellationToken = default)
        {
            return flurlClient.Request(basePath, answerId)
                .GetJsonAsync<TestAnswerDetailsResponse>(cancellationToken);
        }

        public Task CloseAsync(Guid answerId, CancellationToken cancellationToken = default)
        {
            return flurlClient.Request(basePath, answerId, "close")
                .PatchAsync(cancellationToken: cancellationToken);
        }
    }
}