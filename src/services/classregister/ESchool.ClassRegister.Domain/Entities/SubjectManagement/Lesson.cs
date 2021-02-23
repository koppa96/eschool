﻿using System;
using System.Collections.Generic;

namespace ESchool.ClassRegister.Domain.Entities.SubjectManagement
{
    public class Lesson
    {
        public Guid Id { get; set; }

        public int LessonNumber { get; set; }
        public bool Canceled { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime StartsAt { get; set; }
        public DateTime EndsAt { get; set; }

        public Guid ClassSchoolYearSubjectId { get; set; }
        public virtual ClassSchoolYearSubject ClassSchoolYearSubject { get; set; }

        public Guid ClassRoomId { get; set; }
        public virtual ClassRoom ClassRoom { get; set; }

        public virtual ICollection<HomeWork> HomeWorks { get; set; }
        public virtual ICollection<Absence> Absences { get; set; }
    }
}
