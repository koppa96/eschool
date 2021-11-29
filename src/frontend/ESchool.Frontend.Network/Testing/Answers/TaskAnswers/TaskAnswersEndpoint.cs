using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.Testing.Application.Features.TaskAnswers.Common;
using ESchool.Testing.Interface.Features.TaskAnswers.CreateEdit;
using Flurl.Http;

namespace ESchool.Frontend.Network.Testing.Answers.TaskAnswers
{
    public class TaskAnswersEndpoint
    {
        private readonly string basePath;
        private readonly IFlurlClient flurlClient;

        public TaskAnswersEndpoint(string basePath, IFlurlClient flurlClient)
        {
            this.basePath = basePath;
            this.flurlClient = flurlClient;
        }

        public Task<TaskAnswerResponse> GetByIdAsync(Guid taskAnswerId, CancellationToken cancellationToken = default)
        {
            return flurlClient.Request(basePath, taskAnswerId)
                .GetJsonAsync<TaskAnswerResponse>(cancellationToken);
        }

        public Task<TaskAnswerResponse> CreateOrEditTaskAnswer(TaskAnswerCreateEditCommand command,
            CancellationToken cancellationToken = default)
        {
            return flurlClient.Request(basePath)
                .PutJsonAsync(command, cancellationToken)
                .ReceiveJson<TaskAnswerResponse>();
        }

        public Task CorrectAsync(Guid taskAnswerId, int givenPoints, CancellationToken cancellationToken = default)
        {
            return flurlClient.Request(basePath, taskAnswerId)
                .SetQueryParams(new { givenPoints })
                .PatchAsync(cancellationToken: cancellationToken);
        }

        public Task DeleteAsync(Guid taskAnswerId, CancellationToken cancellationToken = default)
        {
            return flurlClient.Request(basePath, taskAnswerId)
                .DeleteAsync(cancellationToken);
        }
    }
}