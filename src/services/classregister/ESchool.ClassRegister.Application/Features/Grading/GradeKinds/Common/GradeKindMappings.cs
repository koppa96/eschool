using AutoMapper;
using ESchool.ClassRegister.Domain.Entities.Grading;
using ESchool.ClassRegister.Interface.Features.Grading.GradeKinds;

namespace ESchool.ClassRegister.Application.Features.Grading.GradeKinds.Common
{
    public class GradeKindMappings : Profile
    {
        public GradeKindMappings()
        {
            CreateMap<GradeKind, GradeKindResponse>();
        }
    }
}