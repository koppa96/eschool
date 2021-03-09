using ESchool.ClassRegister.Domain.Entities.Users;
using System;
using ESchool.ClassRegister.Domain.Enums;
using ESchool.Libs.Domain.Interfaces;

namespace ESchool.ClassRegister.Domain.Entities.SubjectManagement
{
    public class Absence : IEntity
    {
        public Guid Id { get; set; }
        public AbsenceState AbsenceState { get; set; }

        public Guid StudentId { get; set; }
        public virtual Student Student { get; set; }

        public Guid LessonId { get; set; }
        public virtual Lesson Lesson { get; set; }
    }
}
