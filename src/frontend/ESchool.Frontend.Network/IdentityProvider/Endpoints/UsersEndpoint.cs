using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.IdentityProvider.Interface.Features.Users;
using ESchool.Libs.Interface.Response;
using Flurl.Http;

namespace ESchool.Frontend.Network.IdentityProvider.Endpoints
{
    public class UsersEndpoint
    {
        private readonly string basePath;
        private readonly IFlurlClient flurlClient;

        public UsersEndpoint(string basePath, IFlurlClient flurlClient)
        {
            this.basePath = basePath;
            this.flurlClient = flurlClient;
        }

        public Task<PagedListResponse<UserListResponse>> GetPagedListAsync(int pageIndex, int pageSize,
            CancellationToken cancellationToken = default)
        {
            return flurlClient.Request(basePath)
                .SetQueryParams(new { pageIndex, pageSize })
                .GetJsonAsync<PagedListResponse<UserListResponse>>(cancellationToken);
        }

        public Task<UserDetailsResponse> GetDetailsAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return flurlClient.Request(basePath, userId)
                .GetJsonAsync<UserDetailsResponse>(cancellationToken);
        }

        public Task<UserDetailsResponse> CreateAsync(UserCreateCommand command,
            CancellationToken cancellationToken = default)
        {
            return flurlClient.Request(basePath)
                .PostJsonAsync(command, cancellationToken)
                .ReceiveJson<UserDetailsResponse>();
        }

        public Task<UserDetailsResponse> GetMeAsync(CancellationToken cancellationToken = default)
        {
            return flurlClient.Request(basePath, "me")
                .GetJsonAsync<UserDetailsResponse>(cancellationToken);
        }
    }
}