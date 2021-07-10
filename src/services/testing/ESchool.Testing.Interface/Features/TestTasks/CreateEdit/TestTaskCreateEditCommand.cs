using System;
using System.Text.Json.Polymorph.Attributes;
using ESchool.Testing.Interface.Features.TestTasks.Editor;
using MediatR;

namespace ESchool.Testing.Interface.Features.TestTasks.CreateEdit
{
    [JsonBaseClass(DiscriminatorName = TestingConstants.DiscriminatorName)]
    public abstract class TestTaskCreateEditCommand : IRequest<TestTaskEditorResponse>
    {
        public Guid TestId { get; set; }
        
        public string Description { get; set; }

        public int PointValue { get; set; }
        public int IncorrectAnswerPointValue { get; set; }
    }
}