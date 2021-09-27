using System;
using ESchool.ClassRegister.Interface.Features.Users;
using ESchool.Libs.Interface.Response.Common;

namespace ESchool.ClassRegister.Interface.Features.Messaging
{
    public class MessageListResponse
    {
        public Guid Id { get; set; }
        public string Subject { get; set; }
        public DateTime SentAt { get; set; }
        public bool IsRead { get; set; }
        
        public UserRoleListResponse Sender { get; set; }
    }
}