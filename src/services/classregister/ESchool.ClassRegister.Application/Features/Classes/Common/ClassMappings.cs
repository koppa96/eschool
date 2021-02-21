using System.Linq;
using AutoMapper;
using ESchool.ClassRegister.Domain.Entities;

namespace ESchool.ClassRegister.Application.Features.Classes.Common
{
    public class ClassMappings : Profile
    {
        public ClassMappings()
        {
            CreateMap<Class, ClassListResponse>()
                .ForMember(x => x.FinishingSchoolYear, o => o.MapFrom(x => x.DidFinish
                    ? x.ClassSchoolYears.OrderByDescending(x => x.SchoolYear.StartsAt).First().SchoolYear
                    : null));

            CreateMap<Class, ClassDetailsResponse>()
                .ForMember(x => x.StartingSchoolYear, o => o.MapFrom(x => x.ClassSchoolYears.OrderBy(x => x.SchoolYear.StartsAt)
                    .First()
                    .SchoolYear))
                .ForMember(x => x.FinishingSchoolYear, o => o.MapFrom(x => x.DidFinish
                    ? x.ClassSchoolYears.OrderByDescending(x => x.SchoolYear.StartsAt).First().SchoolYear
                    : null));
        }
    }
}