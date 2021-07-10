using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.Libs.Domain.Extensions;
using ESchool.Testing.Application.Features.Tests.Common;
using ESchool.Testing.Domain;
using ESchool.Testing.Interface.Features.Tests;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.Testing.Application.Features.Tests
{
    public class TestCloseHandler : IRequestHandler<TestCloseCommand, TestDetailsResponse>
    {
        private readonly TestingContext context;
        private readonly IMapper mapper;

        public TestCloseHandler(TestingContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        
        public async Task<TestDetailsResponse> Handle(TestCloseCommand request, CancellationToken cancellationToken)
        {
            var test = await context.Tests.Include(x => x.StudentTests)
                .ThenInclude(x => x.TestAnswer)
                .SingleAsync(x => x.Id == request.Id, cancellationToken);
            
            test.Close();

            await context.SaveChangesAsync(cancellationToken);
            return mapper.Map<TestDetailsResponse>(test);
        }
    }
}