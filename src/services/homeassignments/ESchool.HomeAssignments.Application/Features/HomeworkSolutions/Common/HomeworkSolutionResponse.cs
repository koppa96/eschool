using System;
using ESchool.HomeAssignments.Application.Features.HomeworkReviews.Common;

namespace ESchool.HomeAssignments.Application.Features.HomeworkSolutions.Common
{
    public class HomeworkSolutionResponse
    {
        public Guid Id { get; set; }
        public DateTime? TurnInDate { get; set; }
        public HomeworkReviewResponse Review { get; set; }
        
        public class FileResponse
        {
            public Guid Id { get; set; }
            public string FileName { get; set; }
        }
    }
}