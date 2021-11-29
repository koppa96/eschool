using System;
using ESchool.Testing.Interface.Features.TestTasks.Editor;
using MediatR;

namespace ESchool.Testing.Interface.Features.TestTasks
{
    public class TestTaskEditorGetQuery : IRequest<TestTaskEditorResponse>
    {
        public Guid Id { get; set; }
    }
}