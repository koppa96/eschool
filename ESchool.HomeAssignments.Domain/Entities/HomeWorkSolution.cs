using ESchool.HomeAssignments.Domain.Enums;
using System;
using System.Collections.Generic;
using ESchool.HomeAssignments.Domain.Entities.Users;

namespace ESchool.HomeAssignments.Domain.Entities
{
    public class HomeWorkSolution
    {
        public Guid Id { get; set; }

        public Guid HomeWorkId { get; set; }
        public virtual HomeWork HomeWork { get; set; }

        public Guid StudentId { get; set; }
        public virtual Student Student { get; set; }

        public DateTime TurnInDate { get; set; }

        public virtual HomeWorkReview HomeWorkReview { get; set; }
        public virtual ICollection<File> Files { get; set; }
    }
}
