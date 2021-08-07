using System;
using System.Threading.Tasks;

namespace ESchool.Frontend.Network.Authentication
{
    public interface ICodeFlowAuthService
    {
        IObservable<Guid?> TenantId { get; }
        IObservable<bool> HasValidAccessToken { get; }
        IObservable<string> AccessToken { get; }
        
        Task InitAsync();
        Task InitiateCodeFlowAsync();
        Task HandleAuthorizeResponseAsync(string response);
        Task InitiateLogoutAsync();
        Task HandlePostLogoutAsync();
        Task SetTenantIdAsync(Guid? tenantId);
    }
}