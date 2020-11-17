using System;
using System.Collections.Generic;
using ESchool.Libs.Domain.Enums;

namespace ESchool.Libs.Application.Dtos
{
    public class TenantRoleDto
    {
        public Guid TenantId { get; set; }
        public IEnumerable<TenantUserRoleDto> Roles { get; set; }
        
        public class TenantUserRoleDto
        {
            public Guid Id { get; set; }
            public TenantRoleType TenantRoleType { get; set; }
        }
    }
}