using AutoMapper;
using ESchool.Testing.Domain;
using ESchool.Testing.Interface.Features.TestTasks.CreateEdit;

namespace ESchool.Testing.Application.Features.TestTasks.Create
{
    public class TrueOrFalseTestTaskCreateHandler : TestTaskCreateHandler<TrueOrFalseTestTaskCreateEditCommand>
    {
        public TrueOrFalseTestTaskCreateHandler(TestingContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}