using AutoMapper;
using ESchool.Testing.Domain;
using ESchool.Testing.Interface.Features.TestTasks.CreateEdit;

namespace ESchool.Testing.Application.Features.TestTasks.Create
{
    public class FreeTextTestTaskCreateHandler : TestTaskCreateHandler<FreeTextTestTaskCreateEditCommand>
    {
        public FreeTextTestTaskCreateHandler(TestingContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}