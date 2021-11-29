using ESchool.Frontend.Network.Abstractions;

namespace ESchool.Frontend.Network.ClassRegister.Endpoints.Subjects
{
    public class SubjectsChildEndpointSelector
    {
        public SubjectTeachersEndpoint Teachers { get; }
        
        public SubjectsChildEndpointSelector(string basePath, ChildEndpointFactory childEndpointFactory)
        {
            Teachers = childEndpointFactory.CreateChildEndpoint<SubjectTeachersEndpoint>(basePath + "/teachers");
        }
    }
}