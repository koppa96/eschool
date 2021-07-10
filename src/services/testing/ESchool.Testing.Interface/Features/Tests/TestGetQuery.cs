using System;
using MediatR;

namespace ESchool.Testing.Interface.Features.Tests
{
    public class TestGetQuery : IRequest<TestDetailsResponse>
    {
        public Guid TestId { get; set; }
    }
}