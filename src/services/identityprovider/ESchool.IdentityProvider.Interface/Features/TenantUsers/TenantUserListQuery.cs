using System;
using ESchool.IdentityProvider.Interface.Features.Users;
using ESchool.Libs.Interface.Query;

namespace ESchool.IdentityProvider.Interface.Features.TenantUsers
{
    public class TenantUserListQuery : PagedListQuery<UserListResponse>
    {
        public Guid TenantId { get; set; }
    }
}