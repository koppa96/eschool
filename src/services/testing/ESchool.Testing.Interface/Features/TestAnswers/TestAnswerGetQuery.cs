using System;
using MediatR;

namespace ESchool.Testing.Interface.Features.TestAnswers
{
    public class TestAnswerGetQuery : IRequest<TestAnswerDetailsResponse>
    {
        public Guid Id { get; set; }
    }
}