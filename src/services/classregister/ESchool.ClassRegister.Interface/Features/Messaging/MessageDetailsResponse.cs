using System;
using System.Collections.Generic;
using ESchool.ClassRegister.Interface.Features.Users;
using ESchool.Libs.Interface.Response.Common;

namespace ESchool.ClassRegister.Interface.Features.Messaging
{
    public class MessageDetailsResponse
    {
        public Guid Id { get; set; }

        public string Subject { get; set; }
        public string Text { get; set; }
        public DateTime SentAt { get; set; }

        public ClassRegisterUserListResponse Sender { get; set; }
        public List<ClassRegisterUserListResponse> Recipients { get; set; }
    }
}