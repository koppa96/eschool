using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.Libs.Domain.Services;
using ESchool.Testing.Application.Features.TestAnswers.Common;
using ESchool.Testing.Domain;
using ESchool.Testing.Domain.Entities;
using ESchool.Testing.Domain.Entities.Answers;
using ESchool.Testing.Interface.Features.TestAnswers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.Testing.Application.Features.TestAnswers
{
    public class TestAnswerCreateHandler : IRequestHandler<TestAnswerCreateCommand, TestAnswerDetailsResponse>
    {
        private readonly TestingContext context;
        private readonly IIdentityService identityService;
        private readonly IMapper mapper;

        public TestAnswerCreateHandler(TestingContext context, IIdentityService identityService, IMapper mapper)
        {
            this.context = context;
            this.identityService = identityService;
            this.mapper = mapper;
        }
        
        public async Task<TestAnswerDetailsResponse> Handle(TestAnswerCreateCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = identityService.GetCurrentUserId();
            var test = await context.Tests.Include(x => x.Tasks)
                .Include(x => x.StudentTests)
                    .ThenInclude(x => x.TestAnswer)
                .Include(x => x.StudentTests)
                    .ThenInclude(x => x.Student)
                .SingleAsync(x => x.Id == request.TestId, cancellationToken);

            var studentTest = test.StudentTests.Single(x => x.Student.UserId == currentUserId);
            if (studentTest.TestAnswer != null)
            {
                throw new InvalidOperationException("Ehhez a tanulóhoz már létezik dolgozat megoldás.");
            }

            var testAnswer = new TestAnswer
            {
                Id = Guid.NewGuid(),
                Started = DateTime.Now,
                StudentTest = studentTest,
                TaskAnswers = new List<TaskAnswer>()
            };

            context.TestAnswers.Add(testAnswer);
            studentTest.TestAnswer = testAnswer;
            await context.SaveChangesAsync(cancellationToken);

            return mapper.Map<TestAnswerDetailsResponse>(testAnswer);
        }
    }
}