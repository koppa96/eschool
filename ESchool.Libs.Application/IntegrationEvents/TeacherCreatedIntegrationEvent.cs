using System;

namespace ESchool.Libs.Application.IntegrationEvents
{
    public class TeacherCreatedIntegrationEvent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}