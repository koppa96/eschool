using System.Threading;
using System.Threading.Tasks;
using ESchool.Libs.Outbox.Models;

namespace ESchool.Libs.Outbox.Filters
{
    public interface IPublishFilter<TMessage>
    {
        Task Execute(OutboxPublishContext<TMessage> context, CancellationToken cancellationToken = default);
    }
}