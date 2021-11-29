using System;
using ESchool.Libs.Domain.MultiTenancy.Entities;

namespace ESchool.Libs.Domain.Interfaces.Audit
{
    public interface ICreationAudited<TUser, TUserRole>
        where TUser : UserBase<TUser, TUserRole>
        where TUserRole : UserRoleBase<TUser, TUserRole>
    {
        DateTime CreatedAt { get; set; }
        Guid? CreatedById { get; set; }
        TUser CreatedBy { get; set; }
    }
}