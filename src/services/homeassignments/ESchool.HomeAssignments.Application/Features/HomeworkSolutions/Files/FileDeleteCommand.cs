using System;
using ESchool.HomeAssignments.Application.Features.HomeworkSolutions.Common;
using ESchool.HomeAssignments.Interface.Features.HomeworkSolutions;
using MediatR;

namespace ESchool.HomeAssignments.Application.Features.HomeworkSolutions.Files
{
    public class FileDeleteCommand : IRequest<HomeworkSolutionResponse>
    {
        public Guid Id { get; set; }
    }
}