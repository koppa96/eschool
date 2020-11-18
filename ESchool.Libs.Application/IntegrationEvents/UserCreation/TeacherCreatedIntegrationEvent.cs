using System;
using MediatR;

namespace ESchool.Libs.Application.IntegrationEvents.UserCreation
{
    public class TeacherCreatedIntegrationEvent : TenantUserCreatedIntegrationEventBase, IRequest
    {
    }
}