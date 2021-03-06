using AutoMapper;
using ESchool.ClassRegister.Domain.Entities.SubjectManagement;

namespace ESchool.ClassRegister.Application.Features.Lessons.Common
{
    public class LessonMappings : Profile
    {
        public LessonMappings()
        {
            CreateMap<Lesson, LessonDetailsResponse>()
                .ForMember(x => x.Subject, o => o.MapFrom(x => x.ClassSchoolYearSubject.Subject));

            CreateMap<Lesson, LessonListResponse>()
                .ForMember(x => x.Subject, o => o.MapFrom(x => x.ClassSchoolYearSubject.Subject));
        }
    }
}