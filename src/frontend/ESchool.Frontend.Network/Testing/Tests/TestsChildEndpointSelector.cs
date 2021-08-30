using ESchool.Frontend.Network.Abstractions;
using ESchool.Frontend.Network.Testing.Tests.TestAnswers;
using ESchool.Frontend.Network.Testing.Tests.TestTasks;

namespace ESchool.Frontend.Network.Testing.Tests
{
    public class TestsChildEndpointSelector
    {
        public TestAnswersEndpoint Answers { get; }
        public TestTasksEndpoint Tasks { get; }
        
        public TestsChildEndpointSelector(string basePath, ChildEndpointFactory childEndpointFactory)
        {
            Answers = childEndpointFactory.CreateChildEndpoint<TestAnswersEndpoint>(basePath + "/answers");
            Tasks = childEndpointFactory.CreateChildEndpoint<TestTasksEndpoint>(basePath + "/tasks");
        }
    }
}