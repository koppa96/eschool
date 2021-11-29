using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Flurl.Http;

namespace ESchool.Frontend.Network.Abstractions
{
    public abstract class ListingCrudEndpoint<TDto, TCreateDto, TEditDto>
        : CreateUpdateDeleteEndpointBase<TDto, TCreateDto, TEditDto>
    {
        protected ListingCrudEndpoint(IFlurlClient flurlClient) : base(flurlClient)
        {
        }
        
        public Task<List<TDto>> GetAllListAsync(int pageIndex, int pageSize,
            CancellationToken cancellationToken = default)
        {
            return flurlClient.Request(BasePath)
                .SetQueryParams(new { pageIndex, pageSize })
                .GetJsonAsync<List<TDto>>(cancellationToken);
        }
    }
}