using ESchool.Libs.Domain.Interfaces;
using System;

namespace ESchool.Testing.Domain.Entities.ClassRegisterData
{
    public class UserBase : IMultiTenantEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Guid UserId { get; set; }
        public Guid TenantId { get; set; }
    }
}
