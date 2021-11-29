using System.Linq;
using AutoMapper;
using ESchool.ClassRegister.Domain.Entities.SubjectManagement;
using ESchool.ClassRegister.Interface.Features.SubjectManagement.ClassSchoolYearSubjects;

namespace ESchool.ClassRegister.Application.Features.SubjectManagement.ClassSchoolYearSubjects.Common
{
    public class ClassSchoolYearSubjectMappings : Profile
    {
        public ClassSchoolYearSubjectMappings()
        {
            CreateMap<ClassSchoolYearSubject, ClassSchoolYearSubjectDetailsResponse>()
                .ForMember(x => x.Subject, o => o.MapFrom(x => x.Subject))
                .ForMember(x => x.Class, o => o.MapFrom(x => x.ClassSchoolYear.Class))
                .ForMember(x => x.SchoolYear, o => o.MapFrom(x => x.ClassSchoolYear.SchoolYear))
                .ForMember(x => x.Teachers,
                    o => o.MapFrom(x => x.ClassSchoolYearSubjectTeachers.Select(t => t.Teacher)))
                .ForMember(x => x.Students, o => o.MapFrom(x => x.ClassSchoolYear.Class.Students));
        }
    }
}