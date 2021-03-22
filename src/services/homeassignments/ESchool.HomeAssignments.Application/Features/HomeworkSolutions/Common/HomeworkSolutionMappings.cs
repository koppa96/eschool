using AutoMapper;
using ESchool.HomeAssignments.Application.Features.HomeworkReviews.Common;
using ESchool.HomeAssignments.Domain.Entities;

namespace ESchool.HomeAssignments.Application.Features.HomeworkSolutions.Common
{
    public class HomeworkSolutionMappings : Profile
    {
        public HomeworkSolutionMappings()
        {
            CreateMap<HomeworkSolution, HomeworkSolutionResponse>()
                .ForMember(x => x.Review, o => o.MapFrom(x => x.HomeworkReview));

            CreateMap<File, HomeworkSolutionResponse.FileResponse>();

            CreateMap<HomeworkReview, HomeworkReviewResponse>();
        }
    }
}