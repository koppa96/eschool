using System;
using System.Text.Json.Polymorph.Attributes;

namespace ESchool.Testing.Application.Features.TaskAnswers.Common.Details
{
    [JsonBaseClass(DiscriminatorName = "taskType")]
    public class TaskAnswerDetailsResponse
    {
        public Guid Id { get; set; }
    }
}