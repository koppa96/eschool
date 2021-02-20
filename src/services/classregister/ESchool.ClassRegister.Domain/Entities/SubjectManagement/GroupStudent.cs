using System;
using ESchool.ClassRegister.Domain.Entities.Users;

namespace ESchool.ClassRegister.Domain.Entities.SubjectManagement
{
    public class GroupStudent
    {
        public Guid Id { get; set; }

        public Guid StudentId { get; set; }
        public virtual Student Student { get; set; }

        public Guid GroupId { get; set; }
        public virtual Group Group { get; set; }
    }
}