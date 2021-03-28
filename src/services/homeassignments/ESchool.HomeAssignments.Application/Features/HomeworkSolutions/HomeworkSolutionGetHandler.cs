using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ESchool.HomeAssignments.Application.Features.HomeworkSolutions.Common;
using ESchool.HomeAssignments.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.HomeAssignments.Application.Features.HomeworkSolutions
{
    public class HomeworkSolutionGetQuery : IRequest<HomeworkSolutionResponse>
    {
        public Guid HomeworkId { get; set; }
        public Guid StudentId { get; set; }
    }
    
    public class HomeworkSolutionGetHandler : IRequestHandler<HomeworkSolutionGetQuery, HomeworkSolutionResponse>
    {
        private readonly HomeAssignmentsContext context;
        private readonly IConfigurationProvider configurationProvider;

        public HomeworkSolutionGetHandler(HomeAssignmentsContext context, IConfigurationProvider configurationProvider)
        {
            this.context = context;
            this.configurationProvider = configurationProvider;
        }
        
        public Task<HomeworkSolutionResponse> Handle(HomeworkSolutionGetQuery request, CancellationToken cancellationToken)
        {
            return context.HomeworkSolutions.Where(x => x.StudentId == request.StudentId && x.HomeworkId == request.HomeworkId)
                .ProjectTo<HomeworkSolutionResponse>(configurationProvider)
                .SingleAsync(cancellationToken);
        }
    }
}