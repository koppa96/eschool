using System;

namespace ESchool.Libs.AspNetCore.Services
{
    internal class MessagingIdentityService
    {
        public Guid? TenantId { get; set; }
        public Guid? UserId { get; set; }
    }
}