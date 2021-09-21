using AutoMapper;
using ESchool.ClassRegister.Domain.Entities;
using ESchool.ClassRegister.Interface.Features.SchoolYears;

namespace ESchool.ClassRegister.Application.Features.SchoolYears.Common
{
    public class SchoolYearMappings : Profile
    {
        public SchoolYearMappings()
        {
            CreateMap<SchoolYear, SchoolYearDetailsResponse>();
            CreateMap<SchoolYear, SchoolYearListResponse>();
        }
    }
}