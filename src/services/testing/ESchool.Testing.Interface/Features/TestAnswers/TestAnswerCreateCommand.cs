using System;
using MediatR;

namespace ESchool.Testing.Interface.Features.TestAnswers
{
    public class TestAnswerCreateCommand : IRequest<TestAnswerDetailsResponse>
    {
        public Guid TestId { get; set; }
    }
}