using System;
using System.Linq;
using System.Threading.Tasks;
using ESchool.Libs.AspNetCore.Services;
using GreenPipes;
using MassTransit;

namespace ESchool.Libs.AspNetCore.Filters
{
    internal class AuthDataGetterConsumeFilter<T> : IFilter<ConsumeContext<T>>
        where T : class
    {
        private readonly MessagingIdentityService messagingIdentityService;

        public AuthDataGetterConsumeFilter(MessagingIdentityService messagingIdentityService)
        {
            this.messagingIdentityService = messagingIdentityService;
        }
        
        public Task Send(ConsumeContext<T> context, IPipe<ConsumeContext<T>> next)
        {
            messagingIdentityService.UserId = Guid.Parse(context.Headers.Get<string>(MessagingConstants.UserId));
            if (context.Headers.Any(x => x.Key == MessagingConstants.TenantId))
            {
                messagingIdentityService.TenantId = Guid.Parse(context.Headers.Get<string>(MessagingConstants.TenantId));
            }

            return Task.CompletedTask;
        }

        public void Probe(ProbeContext context)
        {
        }
    }
}