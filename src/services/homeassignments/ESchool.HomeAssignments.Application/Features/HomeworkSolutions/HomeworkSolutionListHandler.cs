using System;
using System.Linq;
using AutoMapper;
using ESchool.HomeAssignments.Application.Features.Users.Common;
using ESchool.HomeAssignments.Domain;
using ESchool.HomeAssignments.Domain.Entities;
using ESchool.Libs.Application.Cqrs.Handlers;
using ESchool.Libs.Application.Cqrs.Query;
using Microsoft.EntityFrameworkCore;

namespace ESchool.HomeAssignments.Application.Features.HomeworkSolutions
{
    public class HomeworkSolutionListQuery : PagedListQuery<HomeworkSolutionListResponse>
    {
        public Guid HomeworkId { get; set; }
    }
        
    public class HomeworkSolutionListResponse
    {
        public Guid Id { get; set; }
        public UserListResponse Student { get; set; }
        public DateTime? TurnInDate { get; set; }
        public bool Reviewed { get; set; }
    }

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