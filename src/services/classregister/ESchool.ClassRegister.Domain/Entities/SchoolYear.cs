using ESchool.Libs.Domain.Interfaces;
using System;
using System.Collections.Generic;
using ESchool.ClassRegister.Domain.Entities.SubjectManagement;

namespace ESchool.ClassRegister.Domain.Entities
{
    public class SchoolYear : IMultiTenantEntity
    {
        public Guid Id { get; set; }

        public string DisplayName { get; set; }

        public DateTime StartsAt { get; set; }
        public DateTime EndOfFirstHalf { get; set; }
        public DateTime EndsAt { get; set; }

        public Guid TenantId { get; set; }
        
        public virtual ICollection<ClassSchoolYear> ClassSchoolYears { get; set; }
    }
}
