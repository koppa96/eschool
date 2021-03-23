using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.HomeAssignments.Application.Features.HomeworkSolutions.Common;
using ESchool.HomeAssignments.Domain;
using ESchool.Libs.Domain.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.HomeAssignments.Application.Features.HomeworkSolutions
{
    public class HomeworkSolutionSubmitCommand : IRequest<HomeworkSolutionResponse>
    {
        public Guid Id { get; set; }
    }
    
    public class HomeworkSolutionSubmitHandler : IRequestHandler<HomeworkSolutionSubmitCommand, HomeworkSolutionResponse>
    {
        private readonly HomeAssignmentsContext context;
        private readonly IMapper mapper;

        public HomeworkSolutionSubmitHandler(HomeAssignmentsContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        
        public async Task<HomeworkSolutionResponse> Handle(HomeworkSolutionSubmitCommand request, CancellationToken cancellationToken)
        {
            var solution = await context.HomeworkSolutions.Include(x => x.Files)
                .Include(x => x.Homework)
                .SingleAsync(x => x.Id == request.Id, cancellationToken);

            if (solution.Files.Count == 0)
            {
                throw new InvalidOperationException("Nem adható be üres megoldás.");
            }

            if (solution.Homework.Deadline < DateTime.Now)
            {
                throw new InvalidOperationException("A határidő lejárt.");
            }
            
            solution.TurnInDate = DateTime.Now;
            await context.SaveChangesAsync(cancellationToken);
            return mapper.Map<HomeworkSolutionResponse>(solution);
        }
    }
}