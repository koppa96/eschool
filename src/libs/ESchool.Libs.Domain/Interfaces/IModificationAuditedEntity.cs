using System;

namespace ESchool.Libs.Domain.Interfaces
{
    public interface IModificationAuditedEntity<TNavigation> : IModificationAuditedEntity
        where TNavigation : IUser
    {
        public DateTime? LastModifiedAt { get; set; }
        public Guid? LastModifiedById { get; set; }
        public TNavigation LastModifiedBy { get; set; }

        void IModificationAuditedEntity.SetModification(IUser user)
        {
            if (user is TNavigation navigation)
            {
                LastModifiedAt = DateTime.Now;
                LastModifiedBy = navigation;
            }
            else
            {
                throw new InvalidOperationException($"A user of type {user.GetType()} can not be set as a modifier for {GetType()}.");
            }
        }
    }

    public interface IModificationAuditedEntity : IEntity
    {
        void SetModification(IUser user);
    }
}