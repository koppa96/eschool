using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.HomeAssignments.Application.Features.HomeworkSolutions.Common;
using ESchool.HomeAssignments.Domain;
using ESchool.HomeAssignments.Domain.Entities;
using ESchool.Libs.Domain.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.HomeAssignments.Application.Features.HomeworkSolutions
{
    public class HomeworkSolutionCreateCommand : IRequest<HomeworkSolutionResponse>
    {
        public Guid HomeworkId { get; set; }
    }

    public class HomeworkSolutionCreateHandler : IRequestHandler<HomeworkSolutionCreateCommand, HomeworkSolutionResponse>
    {
        private readonly HomeAssignmentsContext context;
        private readonly IIdentityService identityService;
        private readonly IMapper mapper;

        public HomeworkSolutionCreateHandler(HomeAssignmentsContext context, IIdentityService identityService, IMapper mapper)
        {
            this.context = context;
            this.identityService = identityService;
            this.mapper = mapper;
        }
        
        public async Task<HomeworkSolutionResponse> Handle(HomeworkSolutionCreateCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = identityService.GetCurrentUserId();
            var studentHomework = await context.StudentHomeworks.SingleAsync(
                x => x.Student.UserId == currentUserId && x.HomeworkId == request.HomeworkId, cancellationToken);

            if (studentHomework.HomeworkSolutionId != null)
            {
                throw new InvalidOperationException("Már létrehozásra került egy megoldás ehhez a feladathoz.");
            }

            var solution = new HomeworkSolution
            {
                StudentHomework = studentHomework
            };

            context.HomeworkSolutions.Add(solution);
            await context.SaveChangesAsync(cancellationToken);
            return mapper.Map<HomeworkSolutionResponse>(solution);
        }
    }
}