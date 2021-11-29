using System.Threading;
using System.Threading.Tasks;
using ESchool.Libs.Outbox.Services;
using MediatR;

namespace ESchool.Libs.Outbox.EntityFrameworkCore.Commands
{
    public class DispatchSavedMessagesHandler : IRequestHandler<DispatchSavedMessagesCommand>
    {
        private readonly IMessageDispatcher messageDispatcher;

        public DispatchSavedMessagesHandler(IMessageDispatcher messageDispatcher)
        {
            this.messageDispatcher = messageDispatcher;
        }
        
        public async Task<Unit> Handle(DispatchSavedMessagesCommand request, CancellationToken cancellationToken)
        {
            await messageDispatcher.TryDispatchMessagesAsync(request.MessageIds, cancellationToken);
            return Unit.Value;
        }
    }
}