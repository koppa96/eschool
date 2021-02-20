using System;

namespace ESchool.Testing.Domain.Entities.ClassRegisterData
{
    public class GroupTeacher
    {
        public Guid Id { get; set; }

        public Guid GroupId { get; set; }
        public virtual Group Group { get; set; }

        public Guid TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}