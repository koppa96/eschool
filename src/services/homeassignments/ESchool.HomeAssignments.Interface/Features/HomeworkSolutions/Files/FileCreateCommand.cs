using System;
using System.IO;
using MediatR;

namespace ESchool.HomeAssignments.Interface.Features.HomeworkSolutions.Files
{
    public class FileCreateCommand : IRequest<HomeworkSolutionResponse>
    {
        public Guid SolutionId { get; set; }
        public string FileName { get; set; }
        public Stream FileStream { get; set; }
    }
}