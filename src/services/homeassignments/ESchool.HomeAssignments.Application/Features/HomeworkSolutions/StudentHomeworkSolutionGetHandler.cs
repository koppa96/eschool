using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ESchool.HomeAssignments.Domain;
using ESchool.HomeAssignments.Interface.Features.HomeworkSolutions;
using ESchool.Libs.Domain.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.HomeAssignments.Application.Features.HomeworkSolutions
{
    public class StudentHomeworkSolutionGetQuery : IRequest<HomeworkSolutionResponse>
    {
        public Guid HomeworkId { get; set; }
    }
    
    public class StudentHomeworkSolutionGetHandler : IRequestHandler<StudentHomeworkSolutionGetQuery, HomeworkSolutionResponse>
    {
        private readonly HomeAssignmentsContext context;
        private readonly IIdentityService identityService;
        private readonly IConfigurationProvider configurationProvider;

        public StudentHomeworkSolutionGetHandler(HomeAssignmentsContext context,
            IIdentityService identityService,
            IConfigurationProvider configurationProvider)
        {
            this.context = context;
            this.identityService = identityService;
            this.configurationProvider = configurationProvider;
        }
        
        public Task<HomeworkSolutionResponse> Handle(StudentHomeworkSolutionGetQuery request, CancellationToken cancellationToken)
        {
            var currentUserId = identityService.GetCurrentUserId();
            return context.HomeworkSolutions.Where(x => x.HomeworkId == request.HomeworkId && x.Student.UserId == currentUserId)
                .ProjectTo<HomeworkSolutionResponse>(configurationProvider)
                .SingleOrDefaultAsync(cancellationToken);
        }
    }
}