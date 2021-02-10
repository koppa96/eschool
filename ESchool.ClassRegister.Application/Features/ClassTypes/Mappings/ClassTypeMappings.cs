using AutoMapper;
using ESchool.ClassRegister.Application.Features.ClassTypes.Common;
using ESchool.ClassRegister.Domain.Entities;

namespace ESchool.ClassRegister.Application.Features.ClassTypes.Mappings
{
    public class ClassTypeMappings : Profile
    {
        public ClassTypeMappings()
        {
            CreateMap<ClassType, ClassTypeListResponse>();
        }
    }
}