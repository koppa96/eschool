using System;
using System.Collections.Generic;
using ESchool.ClassRegister.Application.Features.Users.Common;

namespace ESchool.ClassRegister.Application.Features.Messaging.Common
{
    public class MessageDetailsResponse
    {
        public Guid Id { get; set; }

        public string Subject { get; set; }
        public string Text { get; set; }
        public DateTime SentAt { get; set; }

        public UserListResponse Sender { get; set; }
        public List<UserListResponse> Recipients { get; set; }
    }
}