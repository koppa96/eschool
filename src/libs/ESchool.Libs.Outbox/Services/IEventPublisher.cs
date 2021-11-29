using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.Libs.Outbox.Models;

namespace ESchool.Libs.Outbox.Services
{
    public interface IEventPublisher
    {
        void Setup(params object[] @params);
        
        Task PublishAsync(object message, CancellationToken cancellationToken = default);

        Task PublishAsync(object message, Func<OutboxPublishContext, Task> inlineFilter,
            CancellationToken cancellationToken = default);
    }
}