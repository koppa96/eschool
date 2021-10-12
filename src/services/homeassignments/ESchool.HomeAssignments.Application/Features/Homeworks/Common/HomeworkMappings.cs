using AutoMapper;
using ESchool.HomeAssignments.Domain.Entities;
using ESchool.HomeAssignments.Domain.Entities.ClassRegisterData;
using ESchool.HomeAssignments.Interface.Features.Homeworks;

namespace ESchool.HomeAssignments.Application.Features.Homeworks.Common
{
    public class HomeworkMappings : Profile
    {
        public HomeworkMappings()
        {
            CreateMap<Homework, HomeworkDetailsResponse>();

            CreateMap<Lesson, HomeworkDetailsResponse.LessonListResponse>();
        }
    }
}