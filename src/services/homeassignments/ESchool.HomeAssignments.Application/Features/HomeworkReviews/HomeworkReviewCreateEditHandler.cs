using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.HomeAssignments.Domain;
using ESchool.HomeAssignments.Domain.Entities;
using ESchool.HomeAssignments.Interface.Features.HomeworkReviews;
using ESchool.Libs.Domain.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.HomeAssignments.Application.Features.HomeworkReviews
{
    public class HomeworkReviewCreateEditHandler : IRequestHandler<HomeworkReviewCreateEditCommand, HomeworkReviewResponse>
    {
        private readonly HomeAssignmentsContext context;
        private readonly IIdentityService identityService;
        private readonly IMapper mapper;

        public HomeworkReviewCreateEditHandler(HomeAssignmentsContext context, IIdentityService identityService, IMapper mapper)
        {
            this.context = context;
            this.identityService = identityService;
            this.mapper = mapper;
        }
        
        public async Task<HomeworkReviewResponse> Handle(HomeworkReviewCreateEditCommand request, CancellationToken cancellationToken)
        {
            var solution = await context.HomeworkSolutions.Include(x => x.HomeworkReview)
                .Include(x => x.HomeworkReview)
                .Include(x => x.Homework)
                    .ThenInclude(x => x.Lesson)
                        .ThenInclude(x => x.ClassSchoolYearSubject)
                            .ThenInclude(x => x.ClassSchoolYearSubjectTeachers)
                                .ThenInclude(x => x.Teacher)
                .SingleAsync(x => x.Id == request.HomeworkSolutionId, cancellationToken);

            var currentUserId = identityService.GetCurrentUserId();
            var reviewer = solution.Homework.Lesson.ClassSchoolYearSubject.ClassSchoolYearSubjectTeachers.Select(x => x.Teacher)
                .SingleOrDefault(x => x.UserId == currentUserId);
            
            if (reviewer == null)
            {
                throw new UnauthorizedAccessException(
                    "Csak a tárgyat tanító tanár véleményezheti a tárgyhoz tartozó házi feladat megoldásokat.");
            }

            if (solution.HomeworkReview == null)
            {
                var review = new HomeworkReview();
                solution.HomeworkReview = review;
                context.HomeworkReviews.Add(review);
            }

            solution.HomeworkReview.Outcome = request.RequestBody.Outcome;
            solution.HomeworkReview.Comment = request.RequestBody.Comment;
            
            await context.SaveChangesAsync(cancellationToken);
            return mapper.Map<HomeworkReviewResponse>(solution.HomeworkReview);
        }
    }
}