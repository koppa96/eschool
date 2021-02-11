using System;
using ESchool.ClassRegister.Domain.Entities.Users;

namespace ESchool.ClassRegister.Domain.Entities.SubjectManagement
{
    public class SubjectTeacher
    {
        public Guid Id { get; set; }
        
        public Guid SubjectId { get; set; }
        public virtual Subject Subject { get; set; }

        public Guid TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}