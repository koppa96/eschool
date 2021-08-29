using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESchool.IdentityProvider.Interface.Features.Users;
using ESchool.Libs.Domain.Enums;
using ESchool.Libs.Interface.Response;
using Flurl.Http;

namespace ESchool.Frontend.Network.IdentityProvider.Endpoints.Tenants.Users
{
    public class TenantUsersEndpoint
    {
        private readonly string basePath;
        private readonly IFlurlClient flurlClient;

        public TenantUsersEndpoint(string basePath, IFlurlClient flurlClient)
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

        public Task CreateAsync(Guid userId, List<TenantRoleType> tenantRoleTypes,
            CancellationToken cancellationToken = default)
        {
            return flurlClient.Request(basePath, userId)
                .PostJsonAsync(tenantRoleTypes, cancellationToken);
        }

        public Task DeleteAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return flurlClient.Request(basePath, userId)
                .DeleteAsync(cancellationToken);
        }
    }
}