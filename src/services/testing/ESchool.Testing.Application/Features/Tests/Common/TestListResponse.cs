using System;

namespace ESchool.Testing.Application.Features.Tests.Common
{
    public class TestListResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public TestState State { get; set; }
    }
}