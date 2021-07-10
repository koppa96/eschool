using System;
using MediatR;

namespace ESchool.Testing.Interface.Features.TestAnswers
{
    public class TestAnswerCloseCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}