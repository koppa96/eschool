using System;
using MediatR;

namespace ESchool.Testing.Interface.Features.Tests
{
    public class TestCloseCommand : IRequest<TestDetailsResponse>
    {
        public Guid Id { get; set; }
    }
}