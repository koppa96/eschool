using System;
using ESchool.HomeAssignments.Domain.Entities.Users;

namespace ESchool.HomeAssignments.Domain.Entities.ClassRegisterData
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