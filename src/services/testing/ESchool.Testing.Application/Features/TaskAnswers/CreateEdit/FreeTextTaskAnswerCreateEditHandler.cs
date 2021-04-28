using System.Text.Json.Polymorph.Attributes;
using AutoMapper;
using ESchool.Libs.Domain.Services;
using ESchool.Testing.Domain;

namespace ESchool.Testing.Application.Features.TaskAnswers.CreateEdit
{
    [JsonSubClass(DiscriminatorValue = TestingConstants.Discriminators.FreeText)]
    public class FreeTextTaskAnswerCreateEditCommand : TaskAnswerCreateEditCommand
    {
        public string Answer { get; set; }
    }
    
    public class FreeTextTaskAnswerCreateEditHandler : TaskAnswerCreateEditHandler<FreeTextTaskAnswerCreateEditCommand>
    {
        public FreeTextTaskAnswerCreateEditHandler(
            TestingContext context,
            IIdentityService identityService,
            IMapper mapper) : base(context, identityService, mapper)
        {
        }
    }
}