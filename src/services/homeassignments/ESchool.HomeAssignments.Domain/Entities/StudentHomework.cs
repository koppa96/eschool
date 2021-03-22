using System;
using ESchool.HomeAssignments.Domain.Entities.Users;

namespace ESchool.HomeAssignments.Domain.Entities
{
    public class StudentHomework
    {
        public Guid Id { get; set; }

        public Guid StudentId { get; set; }
        public virtual Student Student { get; set; }

        public Guid HomeworkId { get; set; }
        public virtual Homework Homework { get; set; }

        public Guid? HomeworkSolutionId { get; set; }
        public HomeworkSolution HomeworkSolution { get; set; }
    }
}