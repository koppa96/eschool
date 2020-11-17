using System;
using MediatR;

namespace ESchool.Libs.Application.IntegrationEvents
{
    public class TeacherCreatedIntegrationEvent : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}