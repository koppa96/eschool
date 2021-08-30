using System.Threading;
using System.Threading.Tasks;
using ESchool.Frontend.Network.Abstractions;
using ESchool.Frontend.Network.Testing.Answers;
using ESchool.Libs.Interface.Response;
using ESchool.Testing.Interface.Features.TestAnswers;
using Flurl.Http;

namespace ESchool.Frontend.Network.Testing.Tests.TestAnswers
{
    public class TestAnswersEndpoint : AnswersEndpoint
    {
        public TestAnswersEndpoint(string basePath,
            IFlurlClient flurlClient,
            ChildEndpointFactory childEndpointFactory) : base(basePath, flurlClient, childEndpointFactory)
        {
        }

        public Task<PagedListResponse<TestAnswerListResponse>> GetPagedListAsync(int pageIndex, int pageSize,
            CancellationToken cancellationToken = default)
        {
            return flurlClient.Request(basePath)
                .SetQueryParams(new { pageIndex, pageSize })
                .GetJsonAsync<PagedListResponse<TestAnswerListResponse>>(cancellationToken);
        }

        public Task<TestAnswerDetailsResponse> CreateAsync(CancellationToken cancellationToken = default)
        {
            return flurlClient.Request(basePath)
                .PostAsync(cancellationToken: cancellationToken)
                .ReceiveJson<TestAnswerDetailsResponse>();
        }
    }
}