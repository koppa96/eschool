using System;

namespace ESchool.Messaging.Interface.Messages
{
    public class RecipientDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public RecipientType Type { get; set; }
            
        public enum RecipientType
        {
            User,
            Group
        }
    }
}