using System.Linq;
using AutoMapper;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities;
using ESchool.ClassRegister.Interface.Features.SchoolYears;
using ESchool.Libs.Application.Cqrs.Handlers;

namespace ESchool.ClassRegister.Application.Features.SchoolYears
{
    public class SchoolYearListHandler : AutoMapperPagedListHandler<SchoolYearListQuery, SchoolYear, SchoolYearListResponse>
    {
        public SchoolYearListHandler(ClassRegisterContext context, IConfigurationProvider configurationProvider) : base(context, configurationProvider)
        {
        }

        protected override IQueryable<SchoolYear> Filter(IQueryable<SchoolYear> entities, SchoolYearListQuery query)
        {
            if (!string.IsNullOrEmpty(query.Name))
            {
                return entities.Where(x => x.DisplayName.ToLower().Contains(query.Name.ToLower()));
            }

            if (query.Status != null)
            {
                return entities.Where(x => x.Status == query.Status.Value);
            }

            return entities;
        }

        protected override IOrderedQueryable<SchoolYear> Order(IQueryable<SchoolYear> entities)
        {
            return entities.OrderByDescending(x => x.StartsAt);
        }
    }
}