using System;
using System.Collections.Generic;
using MediatR;

namespace ESchool.Testing.Interface.Features.TestTasks
{
    public class TestTaskListQuery : IRequest<List<TestTaskListResponse>>
    {
        public Guid TestId { get; set; }
    }
}