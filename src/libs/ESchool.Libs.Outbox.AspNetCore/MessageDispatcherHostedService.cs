using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using ESchool.Libs.Outbox.AspNetCore.Configuration;
using ESchool.Libs.Outbox.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace ESchool.Libs.Outbox.AspNetCore
{
    internal sealed class MessageDispatcherHostedService : IHostedService, IDisposable
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IOptions<OutboxConfiguration> options;
        private readonly Timer timer;
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        public MessageDispatcherHostedService(IServiceProvider serviceProvider, IOptions<OutboxConfiguration> options)
        {
            this.serviceProvider = serviceProvider;
            this.options = options;
            timer = new Timer(DispatchAsync, null, 0, Timeout.Infinite);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            timer.Change(0, Timeout.Infinite);
            cancellationTokenSource.Cancel();
            return Task.CompletedTask;
        }

        public async void DispatchAsync(object state)
        {
            using var scope = serviceProvider.CreateScope();
            var dispatcher = scope.ServiceProvider.GetRequiredService<IMessageDispatcher>();
            await dispatcher.DispatchAllAsync(cancellationTokenSource.Token);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            timer.Change(options.Value.DispatchDelay, options.Value.DispatchInterval);
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