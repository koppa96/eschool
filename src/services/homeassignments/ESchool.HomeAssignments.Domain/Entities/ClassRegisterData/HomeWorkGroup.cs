using System;
using System.Collections.Generic;

namespace ESchool.HomeAssignments.Domain.Entities.ClassRegisterData
{
    public class HomeWorkGroup
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Guid SubjectId { get; set; }
        public string SubjectName { get; set; }

        public Guid SchoolYearId { get; set; }
        public string SchoolYearDisplayName { get; set; }

        public Guid ClassId { get; set; }
        public string Clas { get; set; }

        public virtual ICollection<Lesson> Lessons { get; set; }
        public virtual ICollection<GroupStudent> GroupStudents { get; set; }
        public virtual ICollection<GroupTeacher> GroupTeachers { get; set; }
    }
}