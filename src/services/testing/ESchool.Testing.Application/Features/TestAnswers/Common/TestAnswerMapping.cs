using System.Linq;
using AutoMapper;
using ESchool.Testing.Domain.Entities;

namespace ESchool.Testing.Application.Features.TestAnswers.Common
{
    public class TestAnswerMapping : Profile
    {
        public TestAnswerMapping()
        {
            CreateMap<TestAnswer, TestAnswerDetailsResponse>()
                .ForMember(x => x.Student, o => o.MapFrom(x => x.StudentTest.Student))
                .ForMember(x => x.HasBeenCorrected, o => o.MapFrom(x => x.TaskAnswers.All(ta => ta.HasBeenCorrected)))
                .ForMember(x => x.Tasks, o => o.MapFrom(x => x.StudentTest.Test.Tasks.Select(t => new
                {
                    Task = t,
                    TaskAnswer = x.TaskAnswers.SingleOrDefault(ta => ta.TestTaskId == t.Id) 
                }).Select(t => new TestAnswerDetailsResponse.TaskStatus
                {
                    TaskId = t.Task.Id,
                    TaskAnswerId = t.TaskAnswer != null ? t.TaskAnswer.Id : null
                }).ToList()));

            CreateMap<TestAnswer, TestAnswerListResponse>()
                .ForMember(x => x.Student, o => o.MapFrom(x => x.StudentTest.Student))
                .ForMember(x => x.HasBeenCorrected, o => o.MapFrom(x => x.TaskAnswers.All(ta => ta.HasBeenCorrected)));
        }
    }
}