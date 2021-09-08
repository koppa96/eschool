using System;
using System.Collections.Generic;
using ESchool.IdentityProvider.Interface.Features.Tenants;
using ESchool.Libs.Domain.Enums;

namespace ESchool.IdentityProvider.Interface.Features.Users
{
    public class UserDetailsResponse
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public Guid? DefaultTenantId { get; set; }
        public GlobalRoleType GlobalRoleType { get; set; }
        public List<TenantListResponse> Tenants { get; set; }
    }
}
