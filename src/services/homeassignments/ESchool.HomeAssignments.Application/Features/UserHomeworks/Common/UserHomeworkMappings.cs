using AutoMapper;
using ESchool.HomeAssignments.Domain.Entities;

namespace ESchool.HomeAssignments.Application.Features.UserHomeworks.Common
{
    public class UserHomeworkMappings : Profile
    {
        public UserHomeworkMappings()
        {
            CreateMap<StudentHomework, StudentHomeworkListResponse>()
                .ForMember(x => x.IsSubmitted,
                    o => o.MapFrom(x => x.HomeworkSolution != null && x.HomeworkSolution.TurnInDate != null))
                .ForMember(x => x.HomeworkReviewOutcome,
                    o => o.MapFrom(x => x.HomeworkSolution.HomeworkReview.Outcome));
        }
    }
}