using AutoMapper;
using ESchool.ClassRegister.Domain.Entities.Grading;
using ESchool.ClassRegister.Interface.Features.Grading.Grades;

namespace ESchool.ClassRegister.Application.Features.Grading.Grades.Common
{
    public class GradeMappings : Profile
    {
        public GradeMappings()
        {
            CreateMap<Grade, GradeDetailsResponse>()
                .ForMember(x => x.Subject, o => o.MapFrom(x => x.ClassSchoolYearSubject.Subject))
                .ForMember(x => x.GradeValue, o => o.MapFrom(x => x.Value))
                .ForMember(x => x.GradeKind, o => o.MapFrom(x => x.Kind));
        }
    }
}