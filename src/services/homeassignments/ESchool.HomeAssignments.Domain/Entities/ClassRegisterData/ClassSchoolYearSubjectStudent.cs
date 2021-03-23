using System;
using ESchool.HomeAssignments.Domain.Entities.Users;

namespace ESchool.HomeAssignments.Domain.Entities.ClassRegisterData
{
    public class ClassSchoolYearSubjectStudent
    {
        public Guid Id { get; set; }

        public Guid ClassSchoolYearSubjectId { get; set; }
        public virtual ClassSchoolYearSubject ClassSchoolYearSubject { get; set; }

        public Guid StudentId { get; set; }
        public virtual Student Student { get; set; }
    }
}