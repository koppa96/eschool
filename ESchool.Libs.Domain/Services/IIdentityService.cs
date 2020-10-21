using System;
using System.Collections.Generic;
using System.Text;
using ESchool.Libs.Domain.Enums;

namespace ESchool.Libs.Domain.Services
{
    public interface IIdentityService
    {
        Guid GetCurrentUserId();
        bool IsInRole(TenantRoleType tenantRoleType);
        Guid? GetTenantId();
    }
}
