using AutoMapper;
using ESchool.ClassRegister.Domain.Entities.SubjectManagement;

namespace ESchool.ClassRegister.Application.Features.SubjectManagement.ClassSubjects.Common
{
    public class ClassSubjectMappings : Profile
    {
        public ClassSubjectMappings()
        {
            CreateMap<ClassSchoolYearSubject, ClassSubjectListResponse>()
                .ForMember(x => x.Class, o => o.MapFrom(x => x.ClassSchoolYear.Class));
        }
    }
}