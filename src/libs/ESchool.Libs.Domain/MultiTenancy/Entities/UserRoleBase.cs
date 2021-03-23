using System;
using ESchool.Libs.Domain.Interfaces;

namespace ESchool.Libs.Domain.MultiTenancy.Entities
{
    public abstract class UserRoleBase<TUser, TUserRole> : IEntity
        where TUser : UserBase<TUser, TUserRole>
        where TUserRole : UserRoleBase<TUser, TUserRole>
    {
        public Guid Id { get; set; }
        
        public Guid UserId { get; set; }
        public virtual TUser User { get; set; }
    }
}