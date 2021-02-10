using AutoMapper;
using ESchool.ClassRegister.Application.Features.SchoolYears.Common;
using ESchool.ClassRegister.Domain.Entities;

namespace ESchool.ClassRegister.Application.Features.SchoolYears.Mapping
{
    public class SchoolYearMappings : Profile
    {
        public SchoolYearMappings()
        {
            CreateMap<SchoolYear, SchoolYearDetailsResponse>();
        }
    }
}