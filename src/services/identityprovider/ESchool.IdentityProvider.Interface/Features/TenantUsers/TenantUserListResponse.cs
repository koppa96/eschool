using System.Collections.Generic;
using ESchool.IdentityProvider.Interface.Features.Users;
using ESchool.Libs.Domain.Enums;

namespace ESchool.IdentityProvider.Interface.Features.TenantUsers
{
    public class TenantUserListResponse : UserListResponse
    {
        public List<TenantRoleType> TenantRoleTypes { get; set; }
    }
}