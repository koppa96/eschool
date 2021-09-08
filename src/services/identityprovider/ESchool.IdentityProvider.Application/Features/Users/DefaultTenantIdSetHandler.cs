using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.IdentityProvider.Domain;
using ESchool.IdentityProvider.Interface.Features.Users;
using ESchool.Libs.Domain.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.IdentityProvider.Application.Features.Users
{
    public class DefaultTenantIdSetHandler : IRequestHandler<DefaultTenantIdSetCommand, UserDetailsResponse>
    {
        private readonly IdentityProviderContext context;
        private readonly IIdentityService identityService;
        private readonly IMapper mapper;

        public DefaultTenantIdSetHandler(IdentityProviderContext context,
            IIdentityService identityService,
            IMapper mapper)
        {
            this.context = context;
            this.identityService = identityService;
            this.mapper = mapper;
        }
        
        public async Task<UserDetailsResponse> Handle(DefaultTenantIdSetCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = identityService.GetCurrentUserId();
            var user = await context.Users.Include(x => x.TenantUsers)
                    .ThenInclude(x => x.Tenant)
                .SingleAsync(x => x.Id == currentUserId, cancellationToken);

            if (user.TenantUsers.All(x => x.TenantId != request.DefaultTenantId))
            {
                throw new InvalidOperationException("Nem állítható be alapértelmezett iskolaként ez az iskola");
            }

            user.DefaultTenantId = request.DefaultTenantId;
            await context.SaveChangesAsync(cancellationToken);

            return mapper.Map<UserDetailsResponse>(user);
        }
    }
}