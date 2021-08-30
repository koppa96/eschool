using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.Frontend.Network.Abstractions;
using ESchool.Testing.Interface.Features.Tests;
using Flurl.Http;

namespace ESchool.Frontend.Network.Testing.Tests
{
    public class TestsEndpoint : CreateUpdateDeleteEndpointBase<TestDetailsResponse, TestCreateCommand, TestEditCommand>
    {
        private readonly ChildEndpointFactory childEndpointFactory;
        protected override string BasePath { get; }

        public TestsChildEndpointSelector this[Guid testId] =>
            childEndpointFactory.CreateChildEndpoint<TestsChildEndpointSelector>(BasePath + $"/{testId}");

        public TestsEndpoint(string basePath,
            IFlurlClient flurlClient,
            ChildEndpointFactory childEndpointFactory) : base(flurlClient)
        {
            this.childEndpointFactory = childEndpointFactory;
            BasePath = basePath;
        }

        public Task<TestDetailsResponse> StartAsync(Guid testId, CancellationToken cancellationToken = default)
        {
            return flurlClient.Request(BasePath, testId, "start")
                .PatchAsync(cancellationToken: cancellationToken)
                .ReceiveJson<TestDetailsResponse>();
        }

        public Task<TestDetailsResponse> CloseAsync(Guid testId, CancellationToken cancellationToken = default)
        {
            return flurlClient.Request(BasePath, testId, "close")
                .PatchAsync(cancellationToken: cancellationToken)
                .ReceiveJson<TestDetailsResponse>();
        }

        public Task CorrectAsync(Guid testId, CancellationToken cancellationToken = default)
        {
            return flurlClient.Request(BasePath, testId, "correct")
                .PatchAsync(cancellationToken: cancellationToken);
        }

        public Task<TestDetailsResponse> GetByIdAsync(Guid testId, CancellationToken cancellationToken = default)
        {
            return flurlClient.Request(BasePath, testId)
                .GetJsonAsync<TestDetailsResponse>(cancellationToken);
        }
    }
}