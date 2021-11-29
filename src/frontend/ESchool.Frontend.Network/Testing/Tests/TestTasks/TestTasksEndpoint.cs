using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESchool.Frontend.Network.Abstractions;
using ESchool.Frontend.Network.Testing.Tasks;
using ESchool.Testing.Interface.Features.TestTasks;
using ESchool.Testing.Interface.Features.TestTasks.CreateEdit;
using ESchool.Testing.Interface.Features.TestTasks.Editor;
using Flurl.Http;

namespace ESchool.Frontend.Network.Testing.Tests.TestTasks
{
    public class TestTasksEndpoint : TasksEndpoint
    {
        public TestTasksEndpoint(string basePath,
            IFlurlClient flurlClient,
            ChildEndpointFactory childEndpointFactory) : base(basePath, flurlClient, childEndpointFactory)
        {
        }

        public Task<List<TestTaskListResponse>> GetAllListAsync(CancellationToken cancellationToken = default)
        {
            return flurlClient.Request(basePath)
                .GetJsonAsync<List<TestTaskListResponse>>(cancellationToken);
        }

        public Task<TestTaskEditorResponse> CreateAsync(TestTaskCreateEditCommand command,
            CancellationToken cancellationToken = default)
        {
            return flurlClient.Request(basePath)
                .PostJsonAsync(command, cancellationToken)
                .ReceiveJson<TestTaskEditorResponse>();
        }
    }
}