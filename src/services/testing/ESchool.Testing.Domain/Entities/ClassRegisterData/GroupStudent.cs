using System;

namespace ESchool.Testing.Domain.Entities.ClassRegisterData
{
    public class GroupStudent
    {
        public Guid Id { get; set; }

        public Guid GroupId { get; set; }
        public virtual Group Group { get; set; }

        public Guid StudentId { get; set; }
        public virtual Student Student { get; set; }
    }
}