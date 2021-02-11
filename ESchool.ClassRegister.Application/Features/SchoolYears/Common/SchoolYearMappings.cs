using AutoMapper;
using ESchool.ClassRegister.Domain.Entities;

namespace ESchool.ClassRegister.Application.Features.SchoolYears.Common
{
    public class SchoolYearMappings : Profile
    {
        public SchoolYearMappings()
        {
            CreateMap<SchoolYear, SchoolYearDetailsResponse>();
        }
    }
}