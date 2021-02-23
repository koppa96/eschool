using System;

namespace ESchool.Libs.Domain.Interfaces
{
    public interface IMultiTenantEntity : IEntity
    {
        public Guid TenantId { get; set; }
    }
}
