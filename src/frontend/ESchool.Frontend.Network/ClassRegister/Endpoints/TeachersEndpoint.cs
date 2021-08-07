using System.Threading;
using System.Threading.Tasks;
using ESchool.Libs.Interface.Response;
using ESchool.Libs.Interface.Response.Common;
using Flurl.Http;

namespace ESchool.Frontend.Network.ClassRegister.Endpoints
{
    public class TeachersEndpoint
    {
        private readonly IFlurlClient flurlClient;
        public const string BasePath = ClassRegisterApi.BasePath + "/teachers";

        public TeachersEndpoint(IFlurlClient flurlClient)
        {
            this.flurlClient = flurlClient;
        }

        public Task<PagedListResponse<UserRoleListResponse>> ListTeachersAsync(
            int pageIndex,
            int pageSize,
            string searchText = null,
            CancellationToken cancellationToken = default)
        {
            return flurlClient.Request(BasePath)
                .SetQueryParams(new { pageIndex, pageSize, searchText })
                .GetJsonAsync<PagedListResponse<UserRoleListResponse>>(cancellationToken);
        }
    }
}