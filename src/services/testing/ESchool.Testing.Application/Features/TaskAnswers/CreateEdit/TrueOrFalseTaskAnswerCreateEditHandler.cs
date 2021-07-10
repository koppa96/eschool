using AutoMapper;
using ESchool.Libs.Domain.Services;
using ESchool.Testing.Domain;
using ESchool.Testing.Interface.Features.TaskAnswers.CreateEdit;

namespace ESchool.Testing.Application.Features.TaskAnswers.CreateEdit
{
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