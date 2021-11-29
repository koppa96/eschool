using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.IdentityProvider.Interface.Features.Tenants;
using ESchool.IdentityProvider.Interface.Features.TenantUsers;
using ESchool.Libs.Application.Cqrs.Authorization;
using ESchool.Libs.Domain.Enums;
using ESchool.Libs.Domain.Services;

namespace ESchool.IdentityProvider.Application.Features.Tenants.Authorization
{
    public class TenantCommandAuthorizationHandler : IRequestAuthorizationHandler<GetTenantQuery>,
        IRequestAuthorizationHandler<EditTenantCommand>,
        IRequestAuthorizationHandler<DeleteTenantCommand>,
        IRequestAuthorizationHandler<TenantUserCreateOrUpdateByIdCommand>,
        IRequestAuthorizationHandler<TenantUserListQuery>,
        IRequestAuthorizationHandler<TenantUserDeleteCommand>
    {
        private readonly IIdentityService identityService;

        public TenantCommandAuthorizationHandler(
            IIdentityService identityService)
        {
            this.identityService = identityService;
        }
        
        private RequestAuthorizationResult CheckUserAuthorizationInTenant(Guid tenantId)
        {
            if (identityService.IsInGlobalRole(GlobalRoleType.TenantAdministrator))
            {
                return RequestAuthorizationResult.Success;
            }

            if (!identityService.IsInRole(TenantRoleType.Administrator))
            {
                return RequestAuthorizationResult.Failure("Az iskolát csak a rendszeradminisztrátorok és adminisztrátorok tekinthetik meg.");
            }

            var userTenantId = identityService.GetTenantId();
            return userTenantId == tenantId
                ? RequestAuthorizationResult.Success
                : RequestAuthorizationResult.Failure(
                    "Az adminisztrátorok csak a saját iskoláik megtekintésére jogosultak.");
        }
        
        public Task<RequestAuthorizationResult> IsAuthorizedAsync(GetTenantQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(CheckUserAuthorizationInTenant(request.TenantId));
        }

        public Task<RequestAuthorizationResult> IsAuthorizedAsync(EditTenantCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(CheckUserAuthorizationInTenant(request.Id));
        }
        
        public Task<RequestAuthorizationResult> IsAuthorizedAsync(TenantUserCreateOrUpdateByIdCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(CheckUserAuthorizationInTenant(request.TenantId));
        }

        public Task<RequestAuthorizationResult> IsAuthorizedAsync(TenantUserListQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(CheckUserAuthorizationInTenant(request.TenantId));
        }
        
        public Task<RequestAuthorizationResult> IsAuthorizedAsync(TenantUserDeleteCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(CheckUserAuthorizationInTenant(request.TenantId));
        }

        public Task<RequestAuthorizationResult> IsAuthorizedAsync(DeleteTenantCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(CheckUserAuthorizationInTenant(request.TenantId));
        }
    }
}