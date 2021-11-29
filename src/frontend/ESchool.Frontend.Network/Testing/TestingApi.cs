using ESchool.Frontend.Network.Abstractions;
using ESchool.Frontend.Network.Testing.Answers;
using ESchool.Frontend.Network.Testing.Tasks;
using ESchool.Frontend.Network.Testing.Tests;

namespace ESchool.Frontend.Network.Testing
{
    public class TestingApi
    {
        public const string BasePath = Api.BasePath + "/testing";

        public TestsEndpoint Tests { get; }
        public AnswersEndpoint Answers { get; }
        public TasksEndpoint Tasks { get; }
        
        public TestingApi(ChildEndpointFactory childEndpointFactory)
        {
            Tests = childEndpointFactory.CreateChildEndpoint<TestsEndpoint>(BasePath + "/tests");
            Answers = childEndpointFactory.CreateChildEndpoint<AnswersEndpoint>(BasePath + "/test-answers");
            Tasks = childEndpointFactory.CreateChildEndpoint<TasksEndpoint>(BasePath + "/tasks");
        }
    }
}