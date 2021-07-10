using System;
using ESchool.HomeAssignments.SharedDomain.Enums;
using ESchool.Libs.Interface.Response.Common;

namespace ESchool.HomeAssignments.Interface.Features.HomeworkReviews
{
    public class HomeworkReviewResponse
    {
        public Guid Id { get; set; }
        public string Comment { get; set; }
        public HomeworkReviewOutcome Outcome { get; set; }
        public DateTime CreatedAt { get; set; }
        public UserRoleListResponse CreatedBy { get; set; }
        public DateTime? LastModifiedAt { get; set; }
        public UserRoleListResponse LastModifiedBy { get; set; }
    }
}