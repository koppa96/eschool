﻿using ESchool.ClassRegister.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESchool.ClassRegister.Domain.Entities.SubjectManagement
{
    public class Absence
    {
        public Guid Id { get; set; }

        public Guid StudentId { get; set; }
        public virtual Student Student { get; set; }

        public Guid LessonId { get; set; }
        public virtual Lesson Lesson { get; set; }
    }
}
