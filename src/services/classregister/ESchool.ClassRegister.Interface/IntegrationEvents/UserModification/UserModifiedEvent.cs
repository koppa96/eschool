using System;

namespace ESchool.ClassRegister.Interface.IntegrationEvents.UserModification
{
    public class UserModifiedEvent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}