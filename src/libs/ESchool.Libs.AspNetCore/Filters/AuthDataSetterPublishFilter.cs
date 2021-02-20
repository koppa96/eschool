using System.Threading.Tasks;
using ESchool.Libs.Domain.Services;
using GreenPipes;
using MassTransit;

namespace ESchool.Libs.AspNetCore.Filters
{
    internal class AuthDataSetterPublishFilter<T> : IFilter<PublishContext<T>>
        where T : class
    {
        private readonly IIdentityService identityService;

        public AuthDataSetterPublishFilter(IIdentityService identityService)
        {
            this.identityService = identityService;
        }
        
        public Task Send(PublishContext<T> context, IPipe<PublishContext<T>> next)
        {
            var tenantId = identityService.TryGetTenantId();
            if (tenantId != null)
            {
                context.Headers.Set(MessagingConstants.TenantId, tenantId.ToString());
            }
            context.Headers.Set(MessagingConstants.UserId, identityService.GetCurrentUserId().ToString());

            return Task.CompletedTask;
        }

        public void Probe(ProbeContext context)
        {
        }
    }
}