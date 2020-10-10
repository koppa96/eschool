using System;

namespace ESchool.Libs.Domain.Interfaces
{
    public interface IMultiTenantEntity
    {
        public Guid TenantId { get; set; }
    }
}
