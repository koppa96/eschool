using System;

namespace ESchool.ClassRegister.Interface.IntegrationEvents.Classes
{
    public class StudentAddedToClassEvent
    {
        public Guid StudentId { get; set; }
        public Guid ClassId { get; set; }
    }
}