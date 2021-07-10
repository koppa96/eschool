using System.Linq;
using AutoMapper;
using ESchool.HomeAssignments.Application.Features.Users.Common;
using ESchool.HomeAssignments.Domain;
using ESchool.HomeAssignments.Domain.Entities;
using ESchool.HomeAssignments.Interface.Features.HomeworkSolutions;
using ESchool.Libs.Application.Cqrs.Handlers;
using Microsoft.EntityFrameworkCore;

namespace ESchool.HomeAssignments.Application.Features.HomeworkSolutions
{
    public class HomeworkSolutionListHandler : AutoMapperPagedListHandler<HomeworkSolutionListQuery, HomeworkSolution, HomeworkSolutionListResponse>
    {
        public HomeworkSolutionListHandler(HomeAssignmentsContext context, IConfigurationProvider configurationProvider) : base(context, configurationProvider)
        {
        }

        protected override IQueryable<HomeworkSolution> Filter(IQueryable<HomeworkSolution> entities, HomeworkSolutionListQuery query)
        {
            return entities.Where(x => x.HomeworkId == query.HomeworkId && x.TurnInDate != null);
        }

        protected override IOrderedQueryable<HomeworkSolution> Order(IQueryable<HomeworkSolution> entities)
        {
            return entities.OrderBy(x => x.TurnInDate);
        }
    }
}