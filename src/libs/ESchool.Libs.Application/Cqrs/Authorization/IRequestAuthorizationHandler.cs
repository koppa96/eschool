using System.Threading;
using System.Threading.Tasks;

namespace ESchool.Libs.Application.Cqrs.Authorization
{
    public interface IRequestAuthorizationHandler<in TRequest>
    {
        Task<RequestAuthorizationResult> IsAuthorizedAsync(TRequest request, CancellationToken cancellationToken);
    }
}