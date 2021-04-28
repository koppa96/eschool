using System.Text.Json.Polymorph.Attributes;
using AutoMapper;
using ESchool.Testing.Domain;

namespace ESchool.Testing.Application.Features.TestTasks.Create
{
    [JsonSubClass(DiscriminatorValue = TestingConstants.Discriminators.TrueOrFalse)]
    public class TrueOrFalseTestTaskCreateEditCommand : TestTaskCreateEditCommand
    {
        public bool IsTrue { get; set; }
    }
    
    public class TrueOrFalseTestTaskCreateHandler : TestTaskCreateHandler<TrueOrFalseTestTaskCreateEditCommand>
    {
        public TrueOrFalseTestTaskCreateHandler(TestingContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}