﻿using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.Libs.Interface.Commands;
using ESchool.Testing.Domain;
using ESchool.Testing.Domain.Entities.Tasks;
using ESchool.Testing.Interface.Features.TestTasks.CreateEdit;
using ESchool.Testing.Interface.Features.TestTasks.Editor;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.Testing.Application.Features.TestTasks.Edit
{
    public class TestTaskEditHandler<TCommand> : IRequestHandler<EditCommand<TCommand, TestTaskEditorResponse>, TestTaskEditorResponse>
        where TCommand : TestTaskCreateEditCommand
    {
        private readonly TestingContext context;
        private readonly IMapper mapper;

        public TestTaskEditHandler(TestingContext context,
            IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        
        public async Task<TestTaskEditorResponse> Handle(EditCommand<TCommand, TestTaskEditorResponse> request, CancellationToken cancellationToken)
        {
            var task = await context.Tasks.Include(x => x.Test)
                .SingleAsync(x => x.Id == request.Id, cancellationToken);

            if (task.Test.StartedAt != null)
            {
                throw new InvalidOperationException("A feladatok nem szerkeszthetők a dolgozat megkezdése után.");
            }

            mapper.Map<TestTaskCreateEditCommand, TestTask>(request.InnerCommand, task);
            await context.SaveChangesAsync(cancellationToken);

            return mapper.Map<TestTask, TestTaskEditorResponse>(task);
        }
    }
}