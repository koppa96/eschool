using System;
using System.Collections.Generic;
using ESchool.Libs.Domain.Interfaces;

namespace ESchool.ClassRegister.Domain.Entities.SubjectManagement
{
    public class Lesson : IEntity
    {
        public Guid Id { get; set; }
        
        public bool Canceled { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime StartsAt { get; set; }
        public DateTime EndsAt { get; set; }

        public Guid ClassSchoolYearSubjectId { get; set; }
        public virtual ClassSchoolYearSubject ClassSchoolYearSubject { get; set; }

        public Guid ClassroomId { get; set; }
        public virtual Classroom Classroom { get; set; }
        
        public virtual ICollection<Absence> Absences { get; set; }
    }
}
