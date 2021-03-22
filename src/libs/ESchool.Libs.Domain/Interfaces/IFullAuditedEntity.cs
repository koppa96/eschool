namespace ESchool.Libs.Domain.Interfaces
{
    public interface IFullAuditedEntity<TNavigation> : ICreationAuditedEntity<TNavigation>, IModificationAuditedEntity<TNavigation>
        where TNavigation : IUser
    {
    }
}