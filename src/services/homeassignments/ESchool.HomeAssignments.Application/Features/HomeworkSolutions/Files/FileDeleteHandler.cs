using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.HomeAssignments.Application.Features.HomeworkSolutions.Common;
using ESchool.HomeAssignments.Domain;
using ESchool.HomeAssignments.Domain.Services;
using ESchool.HomeAssignments.Interface.Features.HomeworkSolutions;
using ESchool.HomeAssignments.Interface.Features.HomeworkSolutions.Files;
using ESchool.Libs.Domain.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.HomeAssignments.Application.Features.HomeworkSolutions.Files
{
    public class FileDeleteHandler : IRequestHandler<FileDeleteCommand, HomeworkSolutionResponse>
    {
        private readonly HomeAssignmentsContext context;
        private readonly ISolutionFileHandlerService solutionFileHandlerService;
        private readonly IMapper mapper;

        public FileDeleteHandler(HomeAssignmentsContext context, ISolutionFileHandlerService solutionFileHandlerService, IMapper mapper)
        {
            this.context = context;
            this.solutionFileHandlerService = solutionFileHandlerService;
            this.mapper = mapper;
        }
        
        public async Task<HomeworkSolutionResponse> Handle(FileDeleteCommand request, CancellationToken cancellationToken)
        {
            var file = await context.Files.Include(x => x.HomeworkSolution)
                    .ThenInclude(x => x.Files)
                .SingleAsync(x => x.Id == request.Id, cancellationToken);

            var solution = file.HomeworkSolution;
            if (solution.TurnInDate != null)
            {
                throw new InvalidOperationException("Ez a feladat már beadásra került, nem lehetséges módosítani.");
            }

            await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
            
            context.Files.Remove(file);
            await context.SaveChangesAsync(cancellationToken);
            await solutionFileHandlerService.DeleteAsync(solution.Id, file.FileName);

            await transaction.CommitAsync(CancellationToken.None);
            return mapper.Map<HomeworkSolutionResponse>(solution);
        }
    }
}