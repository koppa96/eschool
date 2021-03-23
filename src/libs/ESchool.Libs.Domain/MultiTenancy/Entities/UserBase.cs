using System;
using System.Collections.Generic;
using ESchool.Libs.Domain.Interfaces;

namespace ESchool.Libs.Domain.MultiTenancy.Entities
{
    public class UserBase<TUser, TUserRole> : IEntity
        where TUserRole : UserRoleBase<TUser, TUserRole>
        where TUser : UserBase<TUser, TUserRole>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<TUserRole> UserRoles { get; set; }
    }
}