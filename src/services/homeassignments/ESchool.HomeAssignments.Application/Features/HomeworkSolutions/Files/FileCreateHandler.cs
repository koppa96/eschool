using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.HomeAssignments.Application.Features.HomeworkSolutions.Common;
using ESchool.HomeAssignments.Domain;
using ESchool.HomeAssignments.Domain.Entities;
using ESchool.HomeAssignments.Domain.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using File = ESchool.HomeAssignments.Domain.Entities.File;

namespace ESchool.HomeAssignments.Application.Features.HomeworkSolutions.Files
{
    public class FileCreateCommand : IRequest<HomeworkSolutionResponse>
    {
        public Guid SolutionId { get; set; }
        public string FileName { get; set; }
        public Stream FileStream { get; set; }
    }
    
    public class FileCreateHandler : IRequestHandler<FileCreateCommand, HomeworkSolutionResponse>
    {
        private readonly HomeAssignmentsContext context;
        private readonly ISolutionFileHandlerService solutionFileHandlerService;
        private readonly IMapper mapper;

        public FileCreateHandler(HomeAssignmentsContext context,
            ISolutionFileHandlerService solutionFileHandlerService,
            IMapper mapper)
        {
            this.context = context;
            this.solutionFileHandlerService = solutionFileHandlerService;
            this.mapper = mapper;
        }
        
        public async Task<HomeworkSolutionResponse> Handle(FileCreateCommand request, CancellationToken cancellationToken)
        {
            var solution = await context.HomeworkSolutions.Include(x => x.Files)
                .SingleAsync(x => x.Id == request.SolutionId, cancellationToken);

            if (solution.Files.Any(x => x.FileName == request.FileName))
            {
                throw new InvalidOperationException("A fájlneveknek egyedinek kell lenniük.");
            }

            if (solution.TurnInDate != null)
            {
                throw new InvalidOperationException("Ez a feladat már beadásra került, nem lehetséges módosítani.");
            }

            await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

            var file = new File
            {
                FileName = request.FileName,
                HomeWorkSolutionId = request.SolutionId
            };

            context.Files.Add(file);
            await context.SaveChangesAsync(cancellationToken);
            await solutionFileHandlerService.SaveAsync(request.SolutionId, request.FileName, request.FileStream);

            // Nem megszakítható, mivel a fájl már mentésre került
            await transaction.CommitAsync(CancellationToken.None);
            return mapper.Map<HomeworkSolutionResponse>(solution);
        }
    }
}