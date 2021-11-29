using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Application.Features.SubjectManagement.Absences;
using Flurl.Http;

namespace ESchool.Frontend.Network.ClassRegister.Endpoints
{
    public class AbsencesEndpoint
    {
        private readonly IFlurlClient flurlClient;
        
        public const string BasePath = ClassRegisterApi.BasePath + "/absences";
        
        public AbsencesEndpoint(IFlurlClient flurlClient)
        {
            this.flurlClient = flurlClient;
        }

        public Task SetAbsenceStateAsync(
            Guid absenceId,
            AbsenceStateSetCommand command,
            CancellationToken cancellationToken = default)
        {
            return flurlClient.Request(BasePath, absenceId)
                .PostJsonAsync(command, cancellationToken);
        }

        public Task DeleteAsync(
            Guid absenceId,
            CancellationToken cancellationToken = default)
        {
            return flurlClient.Request(BasePath, absenceId)
                .DeleteAsync(cancellationToken);
        }
    }
}