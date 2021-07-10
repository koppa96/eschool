using System;
using System.Text.Json.Polymorph.Attributes;
using ESchool.Testing.Application.Features.TaskAnswers.Common;
using MediatR;

namespace ESchool.Testing.Interface.Features.TaskAnswers.CreateEdit
{
    [JsonBaseClass(DiscriminatorName = TestingConstants.DiscriminatorName)]
    public abstract class TaskAnswerCreateEditCommand : IRequest<TaskAnswerResponse>
    {
        public Guid TaskId { get; set; }
    }
}