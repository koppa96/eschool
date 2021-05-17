using System;
using System.Threading.Tasks;
using ESchool.Libs.Application.Cqrs.Commands;
using ESchool.Libs.Application.Cqrs.Handlers;
using ESchool.Testing.Domain;
using ESchool.Testing.Domain.Entities;

namespace ESchool.Testing.Application.Features.Tests
{
    public class TestDeleteCommand : DeleteCommand
    {
    }
    
    public class TestDeleteHandler : DeleteHandler<TestDeleteCommand, Test>
    {
        public TestDeleteHandler(TestingContext context) : base(context)
        {
        }

        protected override Task ThrowIfCannotDeleteAsync(Test entity)
        {
            if (entity.StartedAt != null)
            {
                throw new InvalidOperationException("Elindított dolgozat nem törölhető.");
            }
            return Task.CompletedTask;
        }
    }
}