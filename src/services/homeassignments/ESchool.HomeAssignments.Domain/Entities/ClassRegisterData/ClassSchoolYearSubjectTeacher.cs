using System;
using ESchool.HomeAssignments.Domain.Entities.Users;

namespace ESchool.HomeAssignments.Domain.Entities.ClassRegisterData
{
    public class ClassSchoolYearSubjectTeacher
    {
        public Guid Id { get; set; }

        public Guid ClassSchoolYearSubjectId { get; set; }
        public virtual ClassSchoolYearSubject ClassSchoolYearSubject { get; set; }

        public Guid TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}