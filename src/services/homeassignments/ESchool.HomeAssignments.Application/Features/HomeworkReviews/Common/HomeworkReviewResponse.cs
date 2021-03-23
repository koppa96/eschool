using System;
using ESchool.HomeAssignments.Application.Features.Users.Common;
using ESchool.HomeAssignments.Domain.Enums;

namespace ESchool.HomeAssignments.Application.Features.HomeworkReviews.Common
{
    public class HomeworkReviewResponse
    {
        public Guid Id { get; set; }
        public string Comment { get; set; }
        public HomeworkReviewOutcome Outcome { get; set; }
        public DateTime CreatedAt { get; set; }
        public UserListResponse CreatedBy { get; set; }
        public DateTime? LastModifiedAt { get; set; }
        public UserListResponse LastModifiedBy { get; set; }
    }
}