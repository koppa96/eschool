using System.Threading;
using System.Threading.Tasks;
using ESchool.Libs.Domain.Services;
using ESchool.Libs.Outbox.Filters;
using ESchool.Libs.Outbox.Models;

namespace ESchool.Libs.AspNetCore.Filters
{
    public class AuthDataSetterPublishFilter<T> : IPublishFilter<T>
    {
        private readonly IIdentityService identityService;

        public AuthDataSetterPublishFilter(IIdentityService identityService)
        {
            this.identityService = identityService;
        }
        
        public Task ExecuteAsync(OutboxPublishContext<T> context, CancellationToken cancellationToken = default)
        {
            var tenantId = identityService.TryGetTenantId();
            if (tenantId != null)
            {
                context.Headers.Add(MessagingConstants.TenantId, tenantId.ToString());
            }
            context.Headers.Add(MessagingConstants.UserId, identityService.GetCurrentUserId().ToString());

            return Task.CompletedTask;
        }
    }
}