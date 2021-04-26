using System;
using System.Linq;
using System.Threading.Tasks;
using ESchool.Libs.Application.Cqrs.Commands;
using ESchool.Libs.Application.Cqrs.Handlers;
using ESchool.Testing.Domain;
using ESchool.Testing.Domain.Entities.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ESchool.Testing.Application.Features.TestTasks
{
    public class TestTaskDeleteCommand : DeleteCommand
    {
    }
    
    public class TestTaskDeleteHandler : DeleteHandler<TestTaskDeleteCommand, TestTask>
    {
        public TestTaskDeleteHandler(TestingContext context) : base(context)
        {
        }

        protected override IQueryable<TestTask> Include(IQueryable<TestTask> entities)
        {
            return entities.Include(x => x.Test);
        }

        protected override Task ThrowIfCannotDeleteAsync(TestTask entity)
        {
            if (entity.Test.StartedAt != null)
            {
                throw new InvalidOperationException("A feladatok nem törölhetők a dolgozat megkezése után.");
            }
            return Task.CompletedTask;
        }
    }
}