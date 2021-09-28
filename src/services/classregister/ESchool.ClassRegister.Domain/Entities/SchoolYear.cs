using ESchool.Libs.Domain.Interfaces;
using System;
using System.Collections.Generic;
using ESchool.ClassRegister.Domain.Entities.SubjectManagement;
using ESchool.ClassRegister.Interface.Features.SchoolYears;

namespace ESchool.ClassRegister.Domain.Entities
{
    public class SchoolYear : IEntity
    {
        public Guid Id { get; set; }

        public string DisplayName { get; set; }

        public DateTime StartsAt { get; set; }
        public DateTime EndOfFirstHalf { get; set; }
        public DateTime EndsAt { get; set; }
        public SchoolYearStatus Status { get; set; }

        public virtual ICollection<ClassSchoolYear> ClassSchoolYears { get; set; }
    }
}
