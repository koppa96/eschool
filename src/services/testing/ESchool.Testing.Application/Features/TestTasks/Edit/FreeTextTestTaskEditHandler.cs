using AutoMapper;
using ESchool.Testing.Application.Features.TestTasks.Create;
using ESchool.Testing.Domain;
using ESchool.Testing.Interface.Features.TestTasks.CreateEdit;

namespace ESchool.Testing.Application.Features.TestTasks.Edit
{
    public class FreeTextTestTaskEditHandler : TestTaskEditHandler<FreeTextTestTaskCreateEditCommand>
    {
        public FreeTextTestTaskEditHandler(TestingContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}