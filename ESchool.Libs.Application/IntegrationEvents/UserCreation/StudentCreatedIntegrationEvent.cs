using MediatR;

namespace ESchool.Libs.Application.IntegrationEvents.UserCreation
{
    public class StudentCreatedIntegrationEvent : TenantUserCreatedIntegrationEventBase, IRequest
    {
    }
}