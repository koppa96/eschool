using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESchool.IdentityProvider.Domain;
using ESchool.IdentityProvider.Interface.Features.Users;
using ESchool.IdentityProvider.Interface.IntegrationEvents.TenantUsers;
using ESchool.Libs.Domain.Enums;
using ESchool.Libs.Domain.Services;
using ESchool.Libs.Outbox.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.IdentityProvider.Application.Features.Users
{
    public class UserDeleteHandler : IRequestHandler<UserDeleteCommand>
    {
        private readonly IdentityProviderContext context;
        private readonly IIdentityService identityService;
        private readonly IEventPublisher eventPublisher;

        public UserDeleteHandler(
            IdentityProviderContext context,
            IIdentityService identityService,
            IEventPublisher eventPublisher)
        {
            this.context = context;
            this.identityService = identityService;
            this.eventPublisher = eventPublisher;
        }
        
        public async Task<Unit> Handle(UserDeleteCommand request, CancellationToken cancellationToken)
        {
            if (!identityService.IsInGlobalRole(GlobalRoleType.TenantAdministrator))
            {
                throw new UnauthorizedAccessException("Csak a rendszeradminisztrátorok törölhetnek felhasználókat!");
            }

            var user = await context.Users.Include(x => x.TenantUsers)
                .SingleAsync(x => x.Id == request.Id, cancellationToken);

            var events = user.TenantUsers.Select(x => new TenantUserDeletedEvent
            {
                TenantId = x.TenantId,
                UserId = x.UserId
            });

            foreach (var @event in events)
            {
                eventPublisher.Setup(context);
                await eventPublisher.PublishAsync(@event, cancellationToken);
            }

            context.Remove(user);
            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}