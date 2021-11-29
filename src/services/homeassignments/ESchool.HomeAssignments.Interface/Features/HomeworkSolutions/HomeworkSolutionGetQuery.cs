using System;
using MediatR;

namespace ESchool.HomeAssignments.Interface.Features.HomeworkSolutions
{
    public class HomeworkSolutionGetQuery : IRequest<HomeworkSolutionResponse>
    {
        public Guid Id { get; set; }
    }
}