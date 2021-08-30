using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.Frontend.Network.Abstractions;
using ESchool.Testing.Interface.Features.TestTasks.CreateEdit;
using ESchool.Testing.Interface.Features.TestTasks.Details;
using ESchool.Testing.Interface.Features.TestTasks.Editor;
using Flurl.Http;

namespace ESchool.Frontend.Network.Testing.Tasks
{
    public class TasksEndpoint
    {
        protected readonly string basePath;
        protected readonly IFlurlClient flurlClient;
        private readonly ChildEndpointFactory childEndpointFactory;

        public TasksEndpoint(string basePath, IFlurlClient flurlClient, ChildEndpointFactory childEndpointFactory)
        {
            this.basePath = basePath;
            this.flurlClient = flurlClient;
            this.childEndpointFactory = childEndpointFactory;
        }

        public Task<TestTaskDetailsResponse> GetByIdAsync(Guid taskId, CancellationToken cancellationToken = default)
        {
            return flurlClient.Request(basePath, taskId)
                .GetJsonAsync<TestTaskDetailsResponse>(cancellationToken);
        }

        public Task<TestTaskEditorResponse> GetEditorViewByIdAsync(Guid taskId,
            CancellationToken cancellationToken = default)
        {
            return flurlClient.Request(basePath, taskId, "edit-view")
                .GetJsonAsync<TestTaskEditorResponse>(cancellationToken);
        }

        public Task<TestTaskEditorResponse> EditAsync(Guid taskId, TestTaskCreateEditCommand command,
            CancellationToken cancellationToken = default)
        {
            return flurlClient.Request(basePath, taskId)
                .PutJsonAsync(command, cancellationToken)
                .ReceiveJson<TestTaskEditorResponse>();
        }

        public Task DeleteAsync(Guid taskId, CancellationToken cancellationToken = default)
        {
            return flurlClient.Request(basePath, taskId)
                .DeleteAsync(cancellationToken);
        }
    }
}