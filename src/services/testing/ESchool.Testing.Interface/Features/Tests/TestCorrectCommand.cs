using System;
using MediatR;

namespace ESchool.Testing.Interface.Features.Tests
{
    public class TestCorrectCommand : IRequest
    {
        public Guid TestId { get; set; }
    }
}