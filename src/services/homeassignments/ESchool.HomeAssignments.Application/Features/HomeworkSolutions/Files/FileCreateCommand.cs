using System;
using System.IO;
using ESchool.HomeAssignments.Application.Features.HomeworkSolutions.Common;
using ESchool.HomeAssignments.Interface.Features.HomeworkSolutions;
using MediatR;

namespace ESchool.HomeAssignments.Application.Features.HomeworkSolutions.Files
{
    public class FileCreateCommand : IRequest<HomeworkSolutionResponse>
    {
        public Guid SolutionId { get; set; }
        public string FileName { get; set; }
        public Stream FileStream { get; set; }
    }
}