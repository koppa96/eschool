using AutoMapper;
using ESchool.Testing.Application.Features.TestTasks.Create;
using ESchool.Testing.Domain;

namespace ESchool.Testing.Application.Features.TestTasks.Edit
{
    public class TrueOrFalseTestTaskEditHandler : TestTaskEditHandler<TrueOrFalseTestTaskCreateEditCommand>
    {
        public TrueOrFalseTestTaskEditHandler(TestingContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}