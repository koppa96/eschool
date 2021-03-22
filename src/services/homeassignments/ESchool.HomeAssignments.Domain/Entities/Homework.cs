﻿using System;
using System.Collections.Generic;
using ESchool.HomeAssignments.Domain.Entities.ClassRegisterData;
using ESchool.Libs.Domain.Interfaces;

namespace ESchool.HomeAssignments.Domain.Entities
{
    public class Homework : IEntity
    {
        public Guid Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime Deadline { get; set; }
        public bool Optional { get; set; }

        public Guid LessonId { get; set; }
        public virtual Lesson Lesson { get; set; }
        
        public virtual ICollection<StudentHomework> StudentHomeworks { get; set; }
        public virtual ICollection<TeacherHomework> TeacherHomeworks { get; set; }
    }
}
