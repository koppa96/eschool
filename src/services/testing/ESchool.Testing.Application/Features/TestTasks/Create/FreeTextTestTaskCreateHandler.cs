using System.Text.Json.Polymorph.Attributes;
using AutoMapper;
using ESchool.Testing.Domain;

namespace ESchool.Testing.Application.Features.TestTasks.Create
{
    [JsonSubClass(DiscriminatorValue = TestingConstants.Discriminators.FreeText)]
    public class FreeTextTestTaskCreateEditCommand : TestTaskCreateEditCommand
    {
        
    }
    
    public class FreeTextTestTaskCreateHandler : TestTaskCreateHandler<FreeTextTestTaskCreateEditCommand>
    {
        public FreeTextTestTaskCreateHandler(TestingContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}