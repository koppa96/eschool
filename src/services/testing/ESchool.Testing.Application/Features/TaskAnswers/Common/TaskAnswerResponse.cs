using System;
using System.Text.Json.Polymorph.Attributes;

namespace ESchool.Testing.Application.Features.TaskAnswers.Common
{
    [JsonBaseClass(DiscriminatorName = TestingConstants.DiscriminatorName)]
    public abstract class TaskAnswerResponse
    {
        public Guid Id { get; set; }
    }
}