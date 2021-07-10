using System;
using MediatR;

namespace ESchool.Testing.Interface.Features.Tests
{
    public class TestStartCommand : IRequest<TestDetailsResponse>
    {
        public Guid Id { get; set; }
    }
}