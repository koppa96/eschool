using System;
using System.Collections.Generic;
using ESchool.HomeAssignments.Domain.Entities.Users;

namespace ESchool.HomeAssignments.Domain.Entities
{
    public class HomeworkSolution
    {
        public Guid Id { get; set; }

        public Guid HomeworkId { get; set; }
        public virtual Homework Homework { get; set; }
        
        public virtual StudentHomework StudentHomework { get; set; }

        public DateTime? TurnInDate { get; set; }

        public virtual HomeworkReview HomeworkReview { get; set; }
        public virtual ICollection<File> Files { get; set; }
    }
}
