using System.Text.Json.Polymorph.Attributes;
using ESchool.Testing.Interface;

namespace ESchool.Testing.Application.Features.TaskAnswers.Common
{
    [JsonSubClass(DiscriminatorValue = TestingConstants.Discriminators.FreeText)]
    public class FreeTextTaskAnswerResponse : TaskAnswerResponse
    {
        public string Answer { get; set; }
    }
}