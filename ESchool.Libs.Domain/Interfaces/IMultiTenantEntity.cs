using System;
using System.Collections.Generic;
using System.Text;

namespace ESchool.Libs.Domain.Interfaces
{
    public interface IMultiTenantEntity
    {
        public Guid TenantId { get; set; }
    }
}
