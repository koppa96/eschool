using AutoMapper;
using ESchool.ClassRegister.Domain.Entities;

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