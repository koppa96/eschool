using System;
using MediatR;

namespace ESchool.HomeAssignments.Interface.Features.HomeworkSolutions
{
    public class StudentHomeworkSolutionGetQuery : IRequest<HomeworkSolutionResponse>
    {
        public Guid HomeworkId { get; set; }
    }
}