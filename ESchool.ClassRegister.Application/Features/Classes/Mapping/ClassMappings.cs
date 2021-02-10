using AutoMapper;
using ESchool.ClassRegister.Application.Features.Classes.Common;
using ESchool.ClassRegister.Domain.Entities;

namespace ESchool.ClassRegister.Application.Features.Classes.Mapping
{
    public class ClassMappings : Profile
    {
        public ClassMappings()
        {
            CreateMap<Class, ClassListResponse>()
                .ForMember(x => x.Grade, o => o.MapFrom(x => x.ClassType.StartingGrade + x.ClassSchoolYears.Count));
        }
    }
}