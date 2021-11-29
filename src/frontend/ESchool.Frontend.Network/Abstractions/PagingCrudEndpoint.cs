using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.Libs.Interface.Response;
using Flurl.Http;

namespace ESchool.Frontend.Network.Abstractions
{
    public abstract class PagingCrudEndpoint<
        TListDto,
        TDetailsDto,
        TCreateDto,
        TEditDto> : CreateUpdateDeleteEndpointBase<TDetailsDto, TCreateDto, TEditDto>
    {
        protected PagingCrudEndpoint(IFlurlClient flurlClient) : base(flurlClient)
        {
        }

        public Task<PagedListResponse<TListDto>> GetPagedListAsync(int pageIndex, int pageSize,
            CancellationToken cancellationToken = default)
        {
            return flurlClient.Request(BasePath)
                .SetQueryParams(new { pageIndex, pageSize })
                .GetJsonAsync<PagedListResponse<TListDto>>(cancellationToken);
        }

        public Task<TDetailsDto> GetDetailsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return flurlClient.Request(BasePath, id)
                .GetJsonAsync<TDetailsDto>(cancellationToken);
        }
    }
}