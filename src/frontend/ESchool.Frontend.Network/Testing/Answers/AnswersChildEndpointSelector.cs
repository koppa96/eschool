using ESchool.Frontend.Network.Abstractions;
using ESchool.Frontend.Network.Testing.Answers.TaskAnswers;

namespace ESchool.Frontend.Network.Testing.Answers
{
    public class AnswersChildEndpointSelector
    {
        public TaskAnswersEndpoint TaskAnswers { get; }
        
        public AnswersChildEndpointSelector(string basePath, ChildEndpointFactory childEndpointFactory)
        {
            TaskAnswers = childEndpointFactory.CreateChildEndpoint<TaskAnswersEndpoint>(basePath + "/task-answers");
        }
    }
}