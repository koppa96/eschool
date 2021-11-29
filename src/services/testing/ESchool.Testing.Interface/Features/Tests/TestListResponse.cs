using System;

namespace ESchool.Testing.Interface.Features.Tests
{
    public class TestListResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public TestState State { get; set; }
    }
}