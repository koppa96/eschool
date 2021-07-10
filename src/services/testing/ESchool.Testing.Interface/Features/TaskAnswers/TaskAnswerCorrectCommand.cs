using System;
using MediatR;

namespace ESchool.Testing.Application.Features.TaskAnswers
{
    public class TaskAnswerCorrectCommand : IRequest
    {
        public Guid TaskAnswerId { get; set; }
        public int GivenPoints { get; set; }
    }
}