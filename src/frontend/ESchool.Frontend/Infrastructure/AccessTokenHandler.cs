using System.Net.Http;
using System.Net.Http.Headers;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESchool.Frontend.Network.Authentication;

namespace ESchool.Frontend.Infrastructure
{
    public class AccessTokenHandler : DelegatingHandler
    {
        private readonly ICodeFlowAuthService codeFlowAuthService;

        public AccessTokenHandler(ICodeFlowAuthService codeFlowAuthService)
        {
            this.codeFlowAuthService = codeFlowAuthService;
        }
        
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var accessToken = await codeFlowAuthService.AccessToken.FirstAsync();
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            return await base.SendAsync(request, cancellationToken);
        }
    }
}