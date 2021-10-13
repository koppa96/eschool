using System;
using ESchool.HomeAssignments.SharedDomain.Enums;
using MediatR;

namespace ESchool.HomeAssignments.Interface.Features.HomeworkReviews
{
    public class HomeworkReviewCreateEditCommand : IRequest<HomeworkReviewResponse>
    {
        public Guid HomeworkSolutionId { get; set; }
        public Body RequestBody { get; set; }
        
        public class Body
        {
            public HomeworkReviewOutcome Outcome { get; set; }
            public string Comment { get; set; }
        }
    }
}