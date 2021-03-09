using System;
using ESchool.HomeAssignments.Domain.Entities.Users;

namespace ESchool.HomeAssignments.Domain.Entities.ClassRegisterData
{
    public class GroupStudent
    {
        public Guid Id { get; set; }
        
        public Guid StudentId { get; set; }
        public virtual Student Student { get; set; }

        public Guid GroupId { get; set; }
        public virtual HomeWorkGroup HomeWorkGroup { get; set; }
    }
}