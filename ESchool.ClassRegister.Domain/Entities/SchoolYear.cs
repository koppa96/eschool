using ESchool.Libs.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESchool.ClassRegister.Domain.Entities
{
    public class SchoolYear : IMultiTenantEntity
    {
        public Guid Id { get; set; }

        public string DisplayName { get; set; }

        public DateTime StartsAt { get; set; }
        public DateTime EndsAt { get; set; }

        public Guid TenantId { get; set; }
    }
}
