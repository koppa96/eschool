﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ESchool.Testing.Domain;
using ESchool.Testing.Interface.Features.TestTasks;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.Testing.Application.Features.TestTasks
{
    public class TestTaskListHandler : IRequestHandler<TestTaskListQuery, List<TestTaskListResponse>>
    {
        private readonly TestingContext context;
        private readonly IConfigurationProvider configurationProvider;

        public TestTaskListHandler(TestingContext context, IConfigurationProvider configurationProvider)
        {
            this.context = context;
            this.configurationProvider = configurationProvider;
        }
        
        public Task<List<TestTaskListResponse>> Handle(TestTaskListQuery request, CancellationToken cancellationToken)
        {
            return context.Tasks.Where(x => x.TestId == request.TestId)
                .ProjectTo<TestTaskListResponse>(configurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}