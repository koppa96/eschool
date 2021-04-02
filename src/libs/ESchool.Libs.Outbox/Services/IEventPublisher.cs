using System;
using System.Threading.Tasks;
using ESchool.Libs.Outbox.Models;

namespace ESchool.Libs.Outbox.Services
{
    public interface IEventPublisher
    {
        Task PublishAsync<TMessage>(TMessage message, Func<OutboxPublishContext<TMessage>, Task> inlineFilter = null);
    }
}