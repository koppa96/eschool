using System;
using System.Collections.Generic;

namespace ESchool.HomeAssignments.Domain.Entities.ClassRegisterData
{
    public class Group
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string SubjectName { get; set; }

        public virtual ICollection<Lesson> Lessons { get; set; }
        public virtual ICollection<GroupStudent> GroupStudents { get; set; }
        public virtual ICollection<GroupTeacher> GroupTeachers { get; set; }
    }
}