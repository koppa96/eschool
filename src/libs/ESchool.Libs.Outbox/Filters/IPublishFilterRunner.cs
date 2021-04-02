using System.Threading;
using System.Threading.Tasks;
using ESchool.Libs.Outbox.Models;

namespace ESchool.Libs.Outbox.Filters
{
    public interface IPublishFilterRunner
    {
        Task<OutboxPublishContext<TMessage>> RunFiltersAsync<TMessage>(TMessage message,
            CancellationToken cancellationToken = default);
    }
}