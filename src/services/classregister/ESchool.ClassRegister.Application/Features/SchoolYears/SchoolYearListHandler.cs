using System.Linq;
using AutoMapper;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities;
using ESchool.ClassRegister.Interface.Features.SchoolYears;
using ESchool.Libs.Application.Cqrs.Handlers;
using ESchool.Libs.Application.Extensions;

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

            if (query.Statuses?.Any() == true)
            {
                return entities.Where(x => query.Statuses.Contains(x.Status));
            }

            return entities;
        }

        protected override IOrderedQueryable<SchoolYear> Order(IQueryable<SchoolYear> entities, SchoolYearListQuery query)
        {
            return query.Orderings?.Any() != true
                ? entities.OrderByDescending(x => x.StartsAt)
                : entities.OrderBy(query);
        }
    }
}