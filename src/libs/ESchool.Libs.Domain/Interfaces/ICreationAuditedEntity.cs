using System;
using ESchool.Libs.Domain.MultiTenancy.Entities;

namespace ESchool.Libs.Domain.Interfaces
{
    public interface ICreationAuditedEntity<TUser, TUserRole>
        where TUser : UserBase<TUser, TUserRole>
        where TUserRole : UserRoleBase<TUser, TUserRole>
    {
        DateTime CreatedAt { get; set; }
        Guid CreatedById { get; set; }
        TUser CreatedBy { get; set; }
    }
}