using System;
using System.Collections.Generic;
using ESchool.IdentityProvider.Application.Features.Tenants;
using ESchool.Libs.Application.Dtos;
using ESchool.Libs.Domain.Enums;

namespace ESchool.IdentityProvider.Application.Features.Users.Common
{
    public class UserDetailsResponse
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public GlobalRoleType GlobalRoleType { get; set; }
        public List<TenantListResponse> Tenants { get; set; }
    }
}
