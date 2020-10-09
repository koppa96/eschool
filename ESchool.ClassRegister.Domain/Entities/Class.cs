using ESchool.Libs.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESchool.ClassRegister.Domain.Entities
{
    public class Class : IMultiTenantEntity
    {
        public Guid TenantId { get; set; }
    }
}
