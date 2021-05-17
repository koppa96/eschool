using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ESchool.Testing.Application.Features.Tests.Common;
using ESchool.Testing.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.Testing.Application.Features.Tests
{
    public class TestGetQuery : IRequest<TestDetailsResponse>
    {
        public Guid TestId { get; set; }
    }
    
    public class TestGetHandler : IRequestHandler<TestGetQuery, TestDetailsResponse>
    {
        private readonly TestingContext context;
        private readonly IConfigurationProvider configurationProvider;

        public TestGetHandler(TestingContext context, IConfigurationProvider configurationProvider)
        {
            this.context = context;
            this.configurationProvider = configurationProvider;
        }
        
        public Task<TestDetailsResponse> Handle(TestGetQuery request, CancellationToken cancellationToken)
        {
            return context.Tests.Where(x => x.Id == request.TestId)
                .ProjectTo<TestDetailsResponse>(configurationProvider)
                .SingleAsync(cancellationToken);
        }
    }
}