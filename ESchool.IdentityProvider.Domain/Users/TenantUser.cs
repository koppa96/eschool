using ESchool.Libs.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESchool.IdentityProvider.Domain.Users
{
    public class TenantUser : User, IMultiTenantEntity
    {
        public Guid TenantId { get; set; }
    }
}
