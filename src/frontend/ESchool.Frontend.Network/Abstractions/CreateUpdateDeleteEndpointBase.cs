using System;
using System.Threading;
using System.Threading.Tasks;
using Flurl.Http;

namespace ESchool.Frontend.Network.Abstractions
{
    public abstract class CreateUpdateDeleteEndpointBase<TDetailsDto, TCreateDto, TEditDto>
    {
        protected readonly IFlurlClient flurlClient;
        
        protected abstract string BasePath { get; }
        
        protected CreateUpdateDeleteEndpointBase(IFlurlClient flurlClient)
        {
            this.flurlClient = flurlClient;
        }

        public Task<TDetailsDto> CreateAsync(TCreateDto createDto, CancellationToken cancellationToken = default)
        {
            return flurlClient.Request(BasePath)
                .PostJsonAsync(createDto, cancellationToken)
                .ReceiveJson<TDetailsDto>();
        }

        public Task<TDetailsDto> EditAsync(Guid id, TEditDto editDto, CancellationToken cancellationToken = default)
        {
            return flurlClient.Request(BasePath, id)
                .PutJsonAsync(editDto, cancellationToken)
                .ReceiveJson<TDetailsDto>();
        }

        public Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            return flurlClient.Request(BasePath, id)
                .DeleteAsync(cancellationToken);
        }
    }
}