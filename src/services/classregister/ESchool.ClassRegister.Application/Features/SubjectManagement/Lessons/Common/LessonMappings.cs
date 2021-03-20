using AutoMapper;
using ESchool.ClassRegister.Domain.Entities.SubjectManagement;

namespace ESchool.ClassRegister.Application.Features.SubjectManagement.Lessons.Common
{
    public class LessonMappings : Profile
    {
        public LessonMappings()
        {
            CreateMap<Lesson, LessonDetailsResponse>()
                .ForMember(x => x.Subject, o => o.MapFrom(x => x.ClassSchoolYearSubject.Subject))
                .ForMember(x => x.Class, o => o.MapFrom(x => x.ClassSchoolYearSubject.ClassSchoolYear.Class))
                .ForMember(x => x.SchoolYear, o => o.MapFrom(x => x.ClassSchoolYearSubject.ClassSchoolYear.SchoolYear));

            CreateMap<Lesson, LessonListResponse>()
                .ForMember(x => x.Subject, o => o.MapFrom(x => x.ClassSchoolYearSubject.Subject));
        }
    }
}