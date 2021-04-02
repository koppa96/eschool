using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ESchool.Libs.Outbox.Services
{
    public interface IMessageDispatcher
    {
        Task DispatchAllAsync(CancellationToken cancellationToken = default);
        Task DispatchMessagesAsync(IEnumerable<Guid> messageIds, CancellationToken cancellationToken = default);
    }
}