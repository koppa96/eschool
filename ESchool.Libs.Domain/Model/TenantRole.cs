using System;
using System.Collections.Generic;
using ESchool.Libs.Domain.Enums;

namespace ESchool.Libs.Domain.Model
{
    public class TenantRole
    {
        public Guid TenantId { get; set; }
        public IEnumerable<TenantRoleType> Roles { get; set; }
    }
}