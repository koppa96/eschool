using ESchool.HomeAssignments.Domain.Enums;
using System;

namespace ESchool.HomeAssignments.Domain.Entities
{
    public class HomeWorkSolution
    {
        public Guid Id { get; set; }

        public Guid HomeWorkId { get; set; }
        public virtual HomeWork HomeWork { get; set; }

        public Guid StudentId { get; set; }

        public HomeWorkSolutionState State { get; set; }
        public int AttemptCount { get; set; }

        public HomeWorkSolution()
        {
            AttemptCount = 1;
        }
    }
}
