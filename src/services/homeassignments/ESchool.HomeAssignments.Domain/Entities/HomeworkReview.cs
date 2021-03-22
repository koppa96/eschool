using System;
using ESchool.HomeAssignments.Domain.Entities.Users;
using ESchool.HomeAssignments.Domain.Enums;
using ESchool.Libs.Domain.Interfaces;

namespace ESchool.HomeAssignments.Domain.Entities
{
    public class HomeworkReview : IFullAuditedEntity<Teacher>
    {
        public Guid Id { get; set; }

        public string Comment { get; set; }
        public HomeworkReviewOutcome Outcome { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public Guid CreatedById { get; set; }
        public Teacher CreatedBy { get; set; }
        
        public DateTime? LastModifiedAt { get; set; }
        public Guid? LastModifiedById { get; set; }
        public Teacher LastModifiedBy { get; set; }
        
        public Guid HomeWorkSolutionId { get; set; }
        public virtual HomeworkSolution HomeworkSolution { get; set; }
    }
}