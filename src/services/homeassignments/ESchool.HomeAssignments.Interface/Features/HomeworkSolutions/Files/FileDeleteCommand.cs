using System;
using MediatR;

namespace ESchool.HomeAssignments.Interface.Features.HomeworkSolutions.Files
{
    public class FileDeleteCommand : IRequest<HomeworkSolutionResponse>
    {
        public Guid Id { get; set; }
    }
}