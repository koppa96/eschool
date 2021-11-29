using ESchool.HomeAssignments.SharedDomain.Enums;

namespace ESchool.HomeAssignments.Interface.Features.HomeworkReviews
{
    public class HomeworkReviewEditCommand
    {
        public string Comment { get; set; }
        public HomeworkReviewOutcome Outcome { get; set; }
    }
}