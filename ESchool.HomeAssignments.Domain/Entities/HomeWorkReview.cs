using System;
using ESchool.HomeAssignments.Domain.Entities.Users;
using ESchool.HomeAssignments.Domain.Enums;

namespace ESchool.HomeAssignments.Domain.Entities
{
    public class HomeWorkReview
    {
        public Guid Id { get; set; }

        public HomeworkReviewOutcome Outcome { get; set; }
        public DateTime? ReviewedAt { get; set; }

        public Guid ReviewerId { get; set; }
        public virtual Teacher Reviewer { get; set; }

        public Guid HomeWorkSolutionId { get; set; }
        public virtual HomeWorkSolution HomeWorkSolution { get; set; }
    }
}