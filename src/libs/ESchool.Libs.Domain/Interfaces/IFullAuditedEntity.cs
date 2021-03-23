using ESchool.Libs.Domain.MultiTenancy.Entities;

namespace ESchool.Libs.Domain.Interfaces
{
    public interface IFullAuditedEntity<TUser, TUserRole> : ICreationAuditedEntity<TUser, TUserRole>, IModificationAuditedEntity<TUser, TUserRole>
        where TUser : UserBase<TUser, TUserRole>
        where TUserRole : UserRoleBase<TUser, TUserRole>
    {
    }
}