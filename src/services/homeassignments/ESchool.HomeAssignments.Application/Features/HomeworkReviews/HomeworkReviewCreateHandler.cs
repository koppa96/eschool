using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.HomeAssignments.Application.Features.HomeworkReviews.Common;
using ESchool.HomeAssignments.Domain;
using ESchool.HomeAssignments.Domain.Entities;
using ESchool.HomeAssignments.Domain.Enums;
using ESchool.Libs.Domain.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.HomeAssignments.Application.Features.HomeworkReviews
{
    public class HomeworkReviewCreateCommand : IRequest<HomeworkReviewResponse>
    {
        public Guid HomeworkSolutionId { get; set; }
        public HomeworkReviewOutcome Outcome { get; set; }
        public string Comment { get; set; }
    }
    
    public class HomeworkReviewCreateHandler : IRequestHandler<HomeworkReviewCreateCommand, HomeworkReviewResponse>
    {
        private readonly HomeAssignmentsContext context;
        private readonly IIdentityService identityService;
        private readonly IMapper mapper;

        public HomeworkReviewCreateHandler(HomeAssignmentsContext context, IIdentityService identityService, IMapper mapper)
        {
            this.context = context;
            this.identityService = identityService;
            this.mapper = mapper;
        }
        
        public async Task<HomeworkReviewResponse> Handle(HomeworkReviewCreateCommand request, CancellationToken cancellationToken)
        {
            var solution = await context.HomeworkSolutions.Include(x => x.HomeworkReview)
                .Include(x => x.Homework)
                    .ThenInclude(x => x.TeacherHomeworks)
                        .ThenInclude(x => x.Teacher)
                .SingleAsync(x => x.Id == request.HomeworkSolutionId, cancellationToken);

            var currentUserId = identityService.GetCurrentUserId();
            var reviewer = solution.Homework.TeacherHomeworks.Select(x => x.Teacher)
                .SingleOrDefault(x => x.UserId == currentUserId);

            if (reviewer == null)
            {
                throw new UnauthorizedAccessException(
                    "Csak a tárgyat tanító tanár véleményezheti a tárgyhoz tartozó házi feladat megoldásokat.");
            }

            if (solution.HomeworkReview != null)
            {
                throw new InvalidOperationException("Ez a feladat már értékelve lett.");
            }

            var review = new HomeworkReview
            {
                Comment = request.Comment,
                Outcome = request.Outcome,
                HomeworkSolution = solution,
                ReviewedAt = DateTime.Now,
                Reviewer = reviewer
            };

            context.HomeworkReviews.Add(review);
            await context.SaveChangesAsync(cancellationToken);
            return mapper.Map<HomeworkReviewResponse>(review);
        }
    }
}