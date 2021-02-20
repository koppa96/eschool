using System;
using System.Collections.Generic;
using ESchool.Libs.Domain.Enums;

namespace ESchool.IdentityProvider.Application.Features.TenantUsers.Common
{
    public class TenantUserDetailsResponse
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public IEnumerable<TenantRoleType> TenantRoles { get; set; }
    }
}