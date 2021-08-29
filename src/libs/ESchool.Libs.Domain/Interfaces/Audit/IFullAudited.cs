using ESchool.Libs.Domain.MultiTenancy.Entities;

namespace ESchool.Libs.Domain.Interfaces.Audit
{
    public interface IFullAudited<TUser, TUserRole> : ICreationAudited<TUser, TUserRole>, IModificationAudited<TUser, TUserRole>
        where TUser : UserBase<TUser, TUserRole>
        where TUserRole : UserRoleBase<TUser, TUserRole>
    {
    }
}