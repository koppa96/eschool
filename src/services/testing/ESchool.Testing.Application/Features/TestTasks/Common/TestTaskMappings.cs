using AutoMapper;
using ESchool.Testing.Application.Features.TestTasks.Create;
using ESchool.Testing.Domain.Entities.Tasks;
using ESchool.Testing.Domain.Entities.Tasks.FreeText;
using ESchool.Testing.Domain.Entities.Tasks.MultipleChoice;
using ESchool.Testing.Domain.Entities.Tasks.TrueOrFalse;
using ESchool.Testing.Interface.Features.TestTasks.CreateEdit;
using ESchool.Testing.Interface.Features.TestTasks.Details;
using ESchool.Testing.Interface.Features.TestTasks.Editor;

namespace ESchool.Testing.Application.Features.TestTasks.Common
{
    public class TestTaskMappings : Profile
    {
        public TestTaskMappings()
        {
            CreateMap<FreeTextTestTask, FreeTextTestTaskDetailsResponse>();
            CreateMap<MultipleChoiceTestTask, MultipleChoiceTestTaskDetailsResponse>();
            CreateMap<TrueOrFalseTestTask, TrueOrFalseTestTaskDetailsResponse>();
            
            CreateMap<TestTask, TestTaskDetailsResponse>()
                .Include<FreeTextTestTask, FreeTextTestTaskDetailsResponse>()
                .Include<MultipleChoiceTestTask, MultipleChoiceTestTaskDetailsResponse>()
                .Include<TrueOrFalseTestTask, TrueOrFalseTestTaskDetailsResponse>();

            CreateMap<FreeTextTestTask, FreeTextTestTaskEditorResponse>();
            CreateMap<MultipleChoiceTestTask, MultipleChoiceTestTaskEditorResponse>();
            CreateMap<TrueOrFalseTestTask, TrueOrFalseTestTaskEditorResponse>();
            
            CreateMap<TestTask, TestTaskEditorResponse>()
                .Include<FreeTextTestTask, FreeTextTestTaskEditorResponse>()
                .Include<MultipleChoiceTestTask, MultipleChoiceTestTaskEditorResponse>()
                .Include<TrueOrFalseTestTask, TrueOrFalseTestTaskEditorResponse>();

            CreateMap<FreeTextTestTaskCreateEditCommand, FreeTextTestTask>();
            CreateMap<TrueOrFalseTestTaskCreateEditCommand, TrueOrFalseTestTask>();
            
            CreateMap<TestTaskCreateEditCommand, TestTask>()
                .Include<FreeTextTestTaskCreateEditCommand, FreeTextTestTask>()
                .Include<TrueOrFalseTestTaskCreateEditCommand, TrueOrFalseTestTask>();
        }
    }
}