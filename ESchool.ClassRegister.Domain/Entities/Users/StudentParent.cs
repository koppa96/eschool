using System;

namespace ESchool.ClassRegister.Domain.Entities.Users
{
    public class StudentParent
    {
        public Guid Id { get; set; }

        public Guid ParentId { get; set; }
        public virtual Parent Parent { get; set; }

        public Guid StudentId { get; set; }
        public virtual Student Student { get; set; }
    }
}
