using AutoMapper;
using ESchool.Testing.Application.Features.TaskAnswers.CreateEdit;
using ESchool.Testing.Domain.Entities.Answers;
using ESchool.Testing.Domain.Entities.Answers.FreeText;
using ESchool.Testing.Domain.Entities.Answers.MultipleChoice;
using ESchool.Testing.Domain.Entities.Answers.TrueOrFalse;

namespace ESchool.Testing.Application.Features.TaskAnswers.Common
{
    public class TaskAnswerMappings : Profile
    {
        public TaskAnswerMappings()
        {
            CreateMap<FreeTextTaskAnswerCreateEditCommand, FreeTextTaskAnswer>();
            CreateMap<MultipleChoiceTaskAnswerCreateEditCommand, MultipleChoiceTaskAnswer>();
            CreateMap<TrueOrFalseTaskAnswerCreateEditCommand, TrueOrFalseTaskAnswer>();
            
            CreateMap<TaskAnswerCreateEditCommand, TaskAnswer>()
                .Include<FreeTextTaskAnswerCreateEditCommand, FreeTextTaskAnswer>()
                .Include<MultipleChoiceTaskAnswerCreateEditCommand, MultipleChoiceTaskAnswer>()
                .Include<TrueOrFalseTaskAnswerCreateEditCommand, TrueOrFalseTaskAnswer>()
                .ForMember(x => x.TestTaskId, o => o.MapFrom(x => x.TaskId));

            CreateMap<FreeTextTaskAnswer, FreeTextTaskAnswerResponse>();
            CreateMap<MultipleChoiceTaskAnswer, MultipleChoiceTaskAnswerResponse>();
            CreateMap<TrueOrFalseTaskAnswer, TrueOrFalseTaskAnswerResponse>();

            CreateMap<TaskAnswer, TaskAnswerResponse>()
                .Include<FreeTextTaskAnswer, FreeTextTaskAnswerResponse>()
                .Include<MultipleChoiceTaskAnswer, MultipleChoiceTaskAnswerResponse>()
                .Include<TrueOrFalseTaskAnswer, TrueOrFalseTaskAnswerResponse>();
        }
    }
}