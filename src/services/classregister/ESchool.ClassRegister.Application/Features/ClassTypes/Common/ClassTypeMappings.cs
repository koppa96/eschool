using AutoMapper;
using ESchool.ClassRegister.Domain.Entities;
using ESchool.ClassRegister.Interface.Features.ClassTypes;

namespace ESchool.ClassRegister.Application.Features.ClassTypes.Common
{
    public class ClassTypeMappings : Profile
    {
        public ClassTypeMappings()
        {
            CreateMap<ClassType, ClassTypeListResponse>();
            CreateMap<ClassType, ClassTypeDetailsResponse>();
        }
    }
}