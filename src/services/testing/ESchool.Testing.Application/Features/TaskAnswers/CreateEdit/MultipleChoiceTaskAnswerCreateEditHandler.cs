using System;
using System.Text.Json.Polymorph.Attributes;
using AutoMapper;
using ESchool.Libs.Domain.Services;
using ESchool.Testing.Domain;

namespace ESchool.Testing.Application.Features.TaskAnswers.CreateEdit
{
    [JsonSubClass(DiscriminatorValue = TestingConstants.Discriminators.MultipleChoice)]
    public class MultipleChoiceTaskAnswerCreateEditCommand : TaskAnswerCreateEditCommand
    {
        public Guid SelectedOptionId { get; set; }
    }
    
    public class MultipleChoiceTaskAnswerCreateEditHandler : TaskAnswerCreateEditHandler<MultipleChoiceTaskAnswerCreateEditCommand>
    {
        public MultipleChoiceTaskAnswerCreateEditHandler(
            TestingContext context,
            IIdentityService identityService,
            IMapper mapper) : base(context, identityService, mapper)
        {
        }
    }
}