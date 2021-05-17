using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ESchool.Testing.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.Testing.Application.Features.TestTasks
{
    public class TestTaskListQuery : IRequest<List<TestTaskListResponse>>
    {
        public Guid TestId { get; set; }
    }

    public class TestTaskListResponse
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
    }

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