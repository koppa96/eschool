using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using ESchool.Libs.Outbox.AspNetCore.Configuration;
using ESchool.Libs.Outbox.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ESchool.Libs.Outbox.AspNetCore
{
    internal sealed class MessageDispatcherHostedService : IHostedService, IDisposable
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IOptions<OutboxConfiguration> options;
        private readonly ILogger<MessageDispatcherHostedService> logger;
        private readonly Timer timer;
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        public MessageDispatcherHostedService(
            IServiceProvider serviceProvider,
            IOptions<OutboxConfiguration> options,
            ILogger<MessageDispatcherHostedService> logger)
        {
            this.serviceProvider = serviceProvider;
            this.options = options;
            this.logger = logger;
            timer = new Timer(DispatchAsync, null, 0, Timeout.Infinite);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            timer.Change(TimeSpan.Zero, TimeSpan.FromSeconds(options.Value.DispatchInterval));
            return Task.CompletedTask;
        }

        public async void DispatchAsync(object state)
        {
            logger.LogDebug("Starting background message dispatch");

            using var scope = serviceProvider.CreateScope();
            var dispatcher = scope.ServiceProvider.GetRequiredService<IMessageDispatcher>();
            await dispatcher.DispatchAllAsync(cancellationTokenSource.Token);


            logger.LogDebug("Background message dispatching finished successfully");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            timer.Change(0, Timeout.Infinite);
            cancellationTokenSource.Cancel();
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            timer?.Dispose();
            cancellationTokenSource?.Cancel();
        }
    }
}