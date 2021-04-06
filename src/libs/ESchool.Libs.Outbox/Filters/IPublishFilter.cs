using System.Threading;
using System.Threading.Tasks;
using ESchool.Libs.Outbox.Models;

namespace ESchool.Libs.Outbox.Filters
{
    public interface IPublishFilter
    {
        Task ExecuteAsync(OutboxPublishContext context, CancellationToken cancellationToken = default);
    }
}