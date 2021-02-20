using System.Linq;
using AutoMapper;
using ESchool.ClassRegister.Domain.Entities;

namespace ESchool.ClassRegister.Application.Features.Subjects.Common
{
    public class SubjectMappings : Profile
    {
        public SubjectMappings()
        {
            CreateMap<Subject, SubjectDetailsResponse>()
                .ForMember(x => x.Teachers, o => o.MapFrom(x => x.SubjectTeachers.Select(st => st.Teacher)));
        }
    }
}