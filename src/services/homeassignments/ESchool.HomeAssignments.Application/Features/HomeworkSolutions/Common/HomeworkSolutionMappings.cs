using AutoMapper;
using ESchool.HomeAssignments.Domain.Entities;
using ESchool.HomeAssignments.Interface.Features.HomeworkReviews;
using ESchool.HomeAssignments.Interface.Features.HomeworkSolutions;

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

            CreateMap<HomeworkSolution, HomeworkSolutionListResponse>()
                .ForMember(x => x.Reviewed, o => o.MapFrom(x => x.HomeworkReview != null));
        }
    }
}