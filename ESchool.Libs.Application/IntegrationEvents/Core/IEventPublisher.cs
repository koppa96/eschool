using System.Threading.Tasks;

namespace ESchool.Libs.Application.IntegrationEvents.Core
{
    public interface IEventPublisher
    {
        Task PublishAsync(object integrationEvent);
    }
}