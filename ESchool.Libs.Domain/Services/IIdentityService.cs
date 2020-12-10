using ESchool.Libs.Domain.Enums;
using System;

namespace ESchool.Libs.Domain.Services
{
    public interface IIdentityService
    {
        Guid GetCurrentUserId();
        bool IsInGlobalRole(GlobalRoleType globalRoleType);
        bool IsInRole(TenantRoleType tenantRoleType);
        Guid GetTenantId();
    }
}
