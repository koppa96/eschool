using System;
using System.Collections.Generic;

namespace ESchool.HomeAssignments.Domain.Entities.ClassRegisterData
{
    public class Lesson
    {
        public Guid Id { get; set; }

        public int LessonNumber { get; set; }
        public string Title { get; set; }

        public Guid GroupId { get; set; }
        public virtual Group Group { get; set; }
        
        public virtual ICollection<HomeWork> HomeWorks { get; set; }
    }
}