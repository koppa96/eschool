using ESchool.ClassRegister.Domain.Entities.Grading;
using System;
using System.Collections.Generic;

namespace ESchool.ClassRegister.Domain.Entities.SubjectManagement
{
    public class Group
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual SmallGradesPolicy SmallGradesPolicy { get; set; }

        public virtual ICollection<ClassSubjectGroup> ClassSubjectGroups { get; set; }

        public virtual ICollection<GroupTeacher> GroupTeachers { get; set; }
        public virtual ICollection<GroupStudent> StudentGroups { get; set; }
        public virtual ICollection<Lesson> Lessons { get; set; }
    }
}
