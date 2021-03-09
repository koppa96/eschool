using AutoMapper;
using ESchool.ClassRegister.Domain.Entities.Grading;

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