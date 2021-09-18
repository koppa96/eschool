using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.IdentityProvider.Domain;
using ESchool.IdentityProvider.Interface.Features.Users;
using ESchool.Libs.Domain.Enums;
using ESchool.Libs.Domain.Extensions;
using ESchool.Libs.Domain.Services;
using ESchool.Libs.Interface.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.IdentityProvider.Application.Features.Users
{
    public class UserEditHandler : IRequestHandler<EditCommand<UserEditCommand, UserDetailsResponse>, UserDetailsResponse>
    {
        private readonly IdentityProviderContext context;
        private readonly IIdentityService identityService;
        private readonly IMapper mapper;

        public UserEditHandler(
            IdentityProviderContext context,
            IIdentityService identityService,
            IMapper mapper)
        {
            this.context = context;
            this.identityService = identityService;
            this.mapper = mapper;
        }
        
        public async Task<UserDetailsResponse> Handle(EditCommand<UserEditCommand, UserDetailsResponse> request, CancellationToken cancellationToken)
        {
            var currentUserId = identityService.GetCurrentUserId();
            if (currentUserId != request.Id && !identityService.IsInGlobalRole(GlobalRoleType.TenantAdministrator))
            {
                throw new UnauthorizedAccessException(
                    "A felhasználó adatait csak a felhasználó és a rendszeradminisztrátor módosíthatja.");
            }

            var user = await context.Users.Include(x => x.TenantUsers)
                    .ThenInclude(x => x.Tenant)
                .Include(x => x.TenantUsers)
                    .ThenInclude(x => x.TenantUserRoles)
                .SingleAsync(x => x.Id == request.Id, cancellationToken);

            if (user.GlobalRole != request.InnerCommand.GlobalRole &&
                !identityService.IsInGlobalRole(GlobalRoleType.TenantAdministrator))
            {
                throw new UnauthorizedAccessException("A szerepkört csak rendszeradminisztrátorok módosíthatják.");
            }

            user.Name = request.InnerCommand.Name;
            user.Email = request.InnerCommand.Email;
            user.GlobalRole = request.InnerCommand.GlobalRole;

            await context.SaveChangesAsync(cancellationToken);
            return mapper.Map<UserDetailsResponse>(user);
        }
    }
}