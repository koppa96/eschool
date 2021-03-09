using AutoMapper;
using ESchool.ClassRegister.Domain.Entities.SubjectManagement;

namespace ESchool.ClassRegister.Application.Features.SubjectManagement.Absences.Common
{
    public class AbsenceMapping : Profile
    {
        public AbsenceMapping()
        {
            CreateMap<Absence, AbsenceListResponse>();
        }        
    }
}