using System;
using ESchool.ClassRegister.Domain.Entities.Users;

namespace ESchool.ClassRegister.Domain.Entities.SubjectManagement
{
    public class GroupTeacher
    {
        public Guid Id { get; set; }

        public Guid TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; }

        public Guid GroupId { get; set; }
        public virtual Group Group { get; set; }
    }
}