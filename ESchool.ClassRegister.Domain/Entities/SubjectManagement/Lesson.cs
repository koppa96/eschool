using System;
using System.Collections.Generic;

namespace ESchool.ClassRegister.Domain.Entities.SubjectManagement
{
    public class Lesson
    {
        public Guid Id { get; set; }

        public DateTime StartsAt { get; set; }
        public DateTime EndsAt { get; set; }

        public Guid GroupId { get; set; }
        public virtual Group Group { get; set; }

        public Guid RoomId { get; set; }
        
        public Guid ClassRoomId { get; set; }
        public virtual ClassRoom ClassRoom { get; set; }

        public virtual ICollection<HomeWork> HomeWorks { get; set; }
        public virtual ICollection<Absence> Absences { get; set; }
    }
}
