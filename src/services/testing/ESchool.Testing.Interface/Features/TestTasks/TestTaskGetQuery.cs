using System;
using ESchool.Testing.Interface.Features.TestTasks.Details;
using MediatR;

namespace ESchool.Testing.Interface.Features.TestTasks
{
    public class TestTaskGetQuery : IRequest<TestTaskDetailsResponse>
    {
        public Guid Id { get; set; }
    }
}