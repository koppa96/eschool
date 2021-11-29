using AutoMapper;
using ESchool.ClassRegister.Domain.Entities.SubjectManagement;
using ESchool.ClassRegister.Interface.Features.SubjectManagement.Absences;

namespace ESchool.ClassRegister.Application.Features.SubjectManagement.Absences.Common
{
    public class AbsenceMapping : Profile
    {
        public AbsenceMapping()
        {
            CreateMap<Absence, AbsenceListResponse>();
            CreateMap<Absence, LessonAbsenceListResponse>();
        }        
    }
}