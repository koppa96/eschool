using ESchool.Frontend.Network.ClassRegister;
using ESchool.Frontend.Network.HomeAssignments;
using ESchool.Frontend.Network.IdentityProvider;
using ESchool.Frontend.Network.Testing;

namespace ESchool.Frontend.Network
{
    public class Api
    {
        public const string BasePath = "/api";
        
        public ClassRegisterApi ClassRegisterApi { get; }
        public HomeAssignmentsApi HomeAssignmentsApi { get; }
        public IdentityProviderApi IdentityProviderApi { get; }
        public TestingApi TestingApi { get; }

        public Api(ClassRegisterApi classRegisterApi,
            HomeAssignmentsApi homeAssignmentsApi,
            IdentityProviderApi identityProviderApi,
            TestingApi testingApi)
        {
            ClassRegisterApi = classRegisterApi;
            HomeAssignmentsApi = homeAssignmentsApi;
            IdentityProviderApi = identityProviderApi;
            TestingApi = testingApi;
        }
    }
}