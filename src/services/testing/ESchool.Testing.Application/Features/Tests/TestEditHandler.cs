using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.Libs.Domain.Extensions;
using ESchool.Libs.Interface.Commands;
using ESchool.Testing.Application.Features.Tests.Common;
using ESchool.Testing.Domain;
using ESchool.Testing.Domain.Entities;
using ESchool.Testing.Interface.Features.Tests;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.Testing.Application.Features.Tests
{
    public class TestEditHandler : IRequestHandler<EditCommand<TestEditCommand, TestDetailsResponse>, TestDetailsResponse>
    {
        private readonly TestingContext context;
        private readonly IMapper mapper;

        public TestEditHandler(TestingContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        
        public async Task<TestDetailsResponse> Handle(EditCommand<TestEditCommand, TestDetailsResponse> request, CancellationToken cancellationToken)
        {
            var test = await context.Tests.Include(x => x.StudentTests)
                .SingleAsync(x => x.Id == request.Id, cancellationToken);

            if (test.StartedAt != null)
            {
                throw new InvalidOperationException("A dolgozat nem szerkeszthető az elindítása után.");
            }
            
            test.Name = request.InnerCommand.Name;
            test.ScheduledStart = request.InnerCommand.ScheduledStart;
            test.ScheduledLength = TimeSpan.FromMinutes(request.InnerCommand.ScheduledLengthInMinutes);

            context.StudentTests.RemoveRange(test.StudentTests);
            context.StudentTests.AddRange(request.InnerCommand.StudentIds.Select(x => new StudentTest
            {
                StudentId = x,
                TestId = test.Id
            }));

            await context.SaveChangesAsync(cancellationToken);
            return mapper.Map<TestDetailsResponse>(test);
        }
    }
}