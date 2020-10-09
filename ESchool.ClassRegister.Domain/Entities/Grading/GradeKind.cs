using ESchool.Libs.Domain.Interfaces;
using System;

namespace ESchool.ClassRegister.Domain.Entities.Grading
{
    public class GradeKind : IMultiTenantEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double AverageMultiplier { get; set; }

        public Guid TenantId { get; set; }
    }
}
