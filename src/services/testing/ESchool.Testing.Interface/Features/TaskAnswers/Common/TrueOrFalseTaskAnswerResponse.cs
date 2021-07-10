using System.Text.Json.Polymorph.Attributes;
using ESchool.Testing.Interface;

namespace ESchool.Testing.Application.Features.TaskAnswers.Common
{
    [JsonSubClass(DiscriminatorValue = TestingConstants.Discriminators.TrueOrFalse)]
    public class TrueOrFalseTaskAnswerResponse : TaskAnswerResponse
    {
        public bool IsTrue { get; set; }
    }
}