using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.Libs.Domain.Extensions;
using ESchool.Testing.Application.Features.Tests.Common;
using ESchool.Testing.Domain;
using ESchool.Testing.Interface.Features.Tests;
using MediatR;

namespace ESchool.Testing.Application.Features.Tests
{
    public class TestStartHandler : IRequestHandler<TestStartCommand, TestDetailsResponse>
    {
        private readonly TestingContext context;
        private readonly IMapper mapper;

        public TestStartHandler(TestingContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        
        public async Task<TestDetailsResponse> Handle(TestStartCommand request, CancellationToken cancellationToken)
        {
            var test = await context.Tests.FindOrThrowAsync(request.Id, cancellationToken);
            
            test.Start();
            
            await context.SaveChangesAsync(cancellationToken);
            return mapper.Map<TestDetailsResponse>(test);
        }
    }
}