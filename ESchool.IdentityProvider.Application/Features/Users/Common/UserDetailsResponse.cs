using System;
using System.Collections.Generic;
using ESchool.Libs.Application.Dtos;
using ESchool.Libs.Domain.Enums;

namespace ESchool.IdentityProvider.Application.Features.Users.Common
{
    public class UserDetailsResponse
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public GlobalRoleType GlobalRoleType { get; set; }
        public IEnumerable<TenantRoleDto> TenantRoles { get; set; }
    }
}
