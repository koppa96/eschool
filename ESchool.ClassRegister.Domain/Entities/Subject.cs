using ESchool.Libs.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESchool.ClassRegister.Domain.Entities
{
    public class Subject : IMultiTenantEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Guid TenantId { get; set; }
    }
}
