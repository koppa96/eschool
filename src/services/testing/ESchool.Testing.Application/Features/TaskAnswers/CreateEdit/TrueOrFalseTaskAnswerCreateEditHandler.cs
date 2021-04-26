using System.Text.Json.Polymorph.Attributes;
using AutoMapper;
using ESchool.Libs.Domain.Services;
using ESchool.Testing.Domain;

namespace ESchool.Testing.Application.Features.TaskAnswers.CreateEdit
{
    [JsonSubClass(DiscriminatorValue = TestingConstants.Discriminators.TrueOrFalse)]
    public class TrueOrFalseTaskAnswerCreateEditCommand : TaskAnswerCreateEditCommand
    {
        public bool IsTrue { get; set; }
    }
    
    public class TrueOrFalseTaskAnswerCreateEditHandler : TaskAnswerCreateEditHandler<TrueOrFalseTaskAnswerCreateEditCommand>
    {
        public TrueOrFalseTaskAnswerCreateEditHandler(
            TestingContext context,
            IIdentityService identityService,
            IMapper mapper) : base(context, identityService, mapper)
        {
        }
    }
}