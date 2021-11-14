using System;
using System.Collections.Generic;
using ESchool.Libs.Domain.Enums;

namespace ESchool.Messaging.Interface.RecipientGroups
{
    public class MessagingUserListResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<TenantRoleType> Roles { get; set; }
    }
}