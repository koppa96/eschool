using AutoMapper;
using ESchool.Libs.Domain.Services;
using ESchool.Testing.Domain;
using ESchool.Testing.Interface.Features.TaskAnswers.CreateEdit;

namespace ESchool.Testing.Application.Features.TaskAnswers.CreateEdit
{
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