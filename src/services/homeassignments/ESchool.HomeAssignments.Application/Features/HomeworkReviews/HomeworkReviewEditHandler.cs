using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.HomeAssignments.Domain;
using ESchool.HomeAssignments.Interface.Features.HomeworkReviews;
using ESchool.Libs.Domain.Extensions;
using ESchool.Libs.Interface.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.HomeAssignments.Application.Features.HomeworkReviews
{
    public class HomeworkReviewEditHandler : IRequestHandler<EditCommand<HomeworkReviewEditCommand, HomeworkReviewResponse>, HomeworkReviewResponse>
    {
        private readonly HomeAssignmentsContext context;
        private readonly IMapper mapper;

        public HomeworkReviewEditHandler(HomeAssignmentsContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        
        public async Task<HomeworkReviewResponse> Handle(EditCommand<HomeworkReviewEditCommand, HomeworkReviewResponse> request, CancellationToken cancellationToken)
        {
            var review = await context.HomeworkReviews.Include(x => x.CreatedBy)
                .Include(x => x.LastModifiedBy)
                .SingleAsync(x => x.Id == request.Id, cancellationToken);
            review.Comment = request.InnerCommand.Comment;
            review.Outcome = request.InnerCommand.Outcome;

            await context.SaveChangesAsync(cancellationToken);
            return mapper.Map<HomeworkReviewResponse>(review);
        }
    }
}