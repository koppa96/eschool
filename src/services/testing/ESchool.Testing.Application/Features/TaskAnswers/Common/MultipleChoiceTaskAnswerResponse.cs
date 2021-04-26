using System;
using System.Text.Json.Polymorph.Attributes;

namespace ESchool.Testing.Application.Features.TaskAnswers.Common
{
    [JsonSubClass(DiscriminatorValue = TestingConstants.Discriminators.MultipleChoice)]
    public class MultipleChoiceTaskAnswerResponse : TaskAnswerResponse
    {
        public Guid SelectedOptionId { get; set; }
    }
}