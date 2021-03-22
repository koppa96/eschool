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
        public DateTime ReviewedAt { get; set; }
        public UserListResponse Reviewer { get; set; }
    }
}