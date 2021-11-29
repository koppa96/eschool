using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using Blazored.SessionStorage;
using ESchool.Frontend.Network.Authentication;
using IdentityModel.Client;
using IdentityModel.OidcClient;
using Microsoft.AspNetCore.Components;

namespace ESchool.Frontend.Infrastructure
{
    public sealed class CodeFlowAuthService : ICodeFlowAuthService, IDisposable
    {
        private const string AuthStateKey = "authState";

        private record AuthState(
            Guid? TenantId,
            string AccessToken,
            AuthorizeState AuthorizeState);

        private readonly ISessionStorageService sessionStorage;
        private readonly OidcClient oidc;
        private readonly NavigationManager navigation;
        private AuthState authState = new(null, null, null);
        private readonly BehaviorSubject<Guid?> tenantId = new(null);
        private readonly BehaviorSubject<string> accessToken = new(null);

        public CodeFlowAuthService(ISessionStorageService sessionStorage,
            OidcClient oidc,
            NavigationManager navigation)
        {
            this.sessionStorage = sessionStorage;
            this.oidc = oidc;
            this.navigation = navigation;
        }

        public IObservable<Guid?> TenantId => tenantId.AsObservable();
        public IObservable<bool> HasValidAccessToken => accessToken.Select(x => x is not null);
        public IObservable<string> AccessToken => accessToken.AsObservable();

        public async Task InitAsync()
        {
            authState = await sessionStorage.GetItemAsync<AuthState>(AuthStateKey);
        }

        public async Task InitiateCodeFlowAsync()
        {
            authState = authState with { AuthorizeState = await oidc.PrepareLoginAsync() };
            await sessionStorage.SetItemAsync(AuthStateKey, authState);
            navigation.NavigateTo(authState.AuthorizeState.StartUrl);
        }

        public async Task HandleAuthorizeResponseAsync(string response)
        {
            var parameters = new Parameters();
            if (TenantId != null)
            {
                parameters.Add(new KeyValuePair<string, string>("tenant_id", TenantId.ToString()));
            }

            var result = await oidc.ProcessResponseAsync(response, authState.AuthorizeState, parameters);
            if (result.IsError)
            {
                throw new ArgumentException("Failed to get access token with the supplied authorization code.");
            }

            authState = authState with
            {
                AccessToken = result.AccessToken,
                AuthorizeState = null
            };
            await sessionStorage.SetItemAsync(AuthStateKey, authState);
        }

        public async Task InitiateLogoutAsync()
        {
            var logoutUrl = await oidc.PrepareLogoutAsync();
            navigation.NavigateTo(logoutUrl);
        }

        public async Task HandlePostLogoutAsync()
        {
            await sessionStorage.RemoveItemAsync(AuthStateKey);
        }

        public async Task SetTenantIdAsync(Guid? tenantId)
        {
            authState = authState with { TenantId = tenantId };
            await sessionStorage.SetItemAsync(AuthStateKey, authState);
        }

        public void Dispose()
        {
            tenantId?.Dispose();
            accessToken?.Dispose();
        }
    }
}