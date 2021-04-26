using System;
using System.Linq;
using System.Text.Json.Polymorph.Attributes;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.Libs.Domain.Services;
using ESchool.Testing.Application.Features.TaskAnswers.Common;
using ESchool.Testing.Domain;
using ESchool.Testing.Domain.Entities.Answers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.Testing.Application.Features.TaskAnswers.CreateEdit
{
    [JsonBaseClass(DiscriminatorName = "taskType")]
    public abstract class TaskAnswerCreateEditCommand : IRequest<TaskAnswerResponse>
    {
        public Guid TaskId { get; set; }
    }

    public abstract class TaskAnswerCreateEditHandler<TRequest> : IRequestHandler<TRequest, TaskAnswerResponse>
        where TRequest : TaskAnswerCreateEditCommand
    {
        private readonly TestingContext context;
        private readonly IIdentityService identityService;
        private readonly IMapper mapper;

        protected TaskAnswerCreateEditHandler(TestingContext context,
            IIdentityService identityService,
            IMapper mapper)
        {
            this.context = context;
            this.identityService = identityService;
            this.mapper = mapper;
        }

        public async Task<TaskAnswerResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            var currentUserId = identityService.GetCurrentUserId();
            var testAnswer = await context.TestAnswers.Include(x => x.TaskAnswers)
                .SingleAsync(x =>
                    x.StudentTest.Student.UserId == currentUserId &&
                    x.StudentTest.Test.Tasks.Any(t => t.Id == request.TaskId), cancellationToken);

            if (testAnswer.Closed != null)
            {
                throw new UnauthorizedAccessException("A beadás nem módosítható, mert már lezárásra került.");
            }

            var answer = testAnswer.TaskAnswers.SingleOrDefault(x => x.TestTaskId == request.TaskId);
            if (answer == null)
            {
                answer = mapper.Map<TaskAnswerCreateEditCommand, TaskAnswer>(request);
                context.TaskAnswers.Add(answer);
            }
            else
            {
                mapper.Map<TaskAnswerCreateEditCommand, TaskAnswer>(request, answer);
            }

            await context.SaveChangesAsync(cancellationToken);
            return mapper.Map<TaskAnswer, TaskAnswerResponse>(answer);
        }
    }
}