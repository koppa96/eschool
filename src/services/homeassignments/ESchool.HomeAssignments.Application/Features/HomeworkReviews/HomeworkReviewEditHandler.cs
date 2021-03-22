using System.Threading;
using System.Threading.Tasks;
using ESchool.HomeAssignments.Application.Features.HomeworkReviews.Common;
using ESchool.HomeAssignments.Domain;
using ESchool.HomeAssignments.Domain.Enums;
using ESchool.Libs.Application.Cqrs.Commands;
using ESchool.Libs.Domain.Extensions;
using MediatR;

namespace ESchool.HomeAssignments.Application.Features.HomeworkReviews
{
    public class HomeworkReviewEditCommand
    {
        public string Comment { get; set; }
        public HomeworkReviewOutcome Outcome { get; set; }
    }
    
    public class HomeworkReviewEditHandler : IRequestHandler<EditCommand<HomeworkReviewEditCommand, HomeworkReviewResponse>, HomeworkReviewResponse>
    {
        private readonly HomeAssignmentsContext context;

        public HomeworkReviewEditHandler(HomeAssignmentsContext context)
        {
            this.context = context;
        }
        
        public async Task<HomeworkReviewResponse> Handle(EditCommand<HomeworkReviewEditCommand, HomeworkReviewResponse> request, CancellationToken cancellationToken)
        {
            var review = await context.HomeworkReviews.FindOrThrowAsync(request.Id, cancellationToken);
            review.Comment = request.InnerCommand.Comment;
            review.Outcome = request.InnerCommand.Outcome;
        }
    }
}