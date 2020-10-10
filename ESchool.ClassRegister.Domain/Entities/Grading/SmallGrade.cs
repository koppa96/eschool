using ESchool.ClassRegister.Domain.Entities.SubjectManagement;
using ESchool.ClassRegister.Domain.Entities.Users;
using ESchool.ClassRegister.Domain.Enums;
using System;

namespace ESchool.ClassRegister.Domain.Entities.Grading
{
    public class SmallGrade
    {
        public Guid Id { get; set; }

        public GradeValue Value { get; set; }
        public string Description { get; set; }

        public Guid? StudentId { get; set; }
        public virtual Student Student { get; set; }

        public Guid ClassSubjectId { get; set; }
        public virtual ClassSubject ClassSubject { get; set; }

        public Guid? TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}
