using System;
using ESchool.HomeAssignments.Domain.Entities.Users;

namespace ESchool.HomeAssignments.Domain.Entities
{
    public class TeacherHomework
    {
        public Guid Id { get; set; }

        public Guid HomeworkId { get; set; }
        public virtual Homework Homework { get; set; }

        public Guid TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}