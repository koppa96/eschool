using ESchool.Libs.Domain.Enums;
using ESchool.Libs.Domain.MultiTenancy.Entities;

namespace ESchool.Messaging.Domain.Entities
{
    public class MessagingUserRole : UserRoleBase<MessagingUser, MessagingUserRole>
    {
        public TenantRoleType TenantRoleType { get; set; }
    }
}