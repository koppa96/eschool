using System;
using ESchool.Testing.Application.Features.TaskAnswers.Common;
using MediatR;

namespace ESchool.Testing.Application.Features.TaskAnswers
{
    public class TaskAnswerGetQuery : IRequest<TaskAnswerResponse>
    {
        public Guid Id { get; set; }
    }
}