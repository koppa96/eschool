using System.Collections.Generic;
using ESchool.Libs.Domain.Enums;

namespace ESchool.IdentityProvider.Interface.Features.TenantUsers
{
    public class TenantUserEditCommand
    {
        public IEnumerable<TenantRoleType> TenantRoleTypes { get; set; }
    }
}