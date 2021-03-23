using System;
using ESchool.Libs.Domain.MultiTenancy.Entities;

namespace ESchool.Libs.Domain.Interfaces
{
    public interface IModificationAuditedEntity<TUser, TUserRole>
        where TUser : UserBase<TUser, TUserRole>
        where TUserRole : UserRoleBase<TUser, TUserRole>
    {
        public DateTime? LastModifiedAt { get; set; }
        public Guid? LastModifiedById { get; set; }
        public TUser LastModifiedBy { get; set; }
    }
}