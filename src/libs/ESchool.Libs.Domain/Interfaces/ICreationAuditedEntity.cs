using System;

namespace ESchool.Libs.Domain.Interfaces
{
    public interface ICreationAuditedEntity<TNavigation> : ICreationAuditedEntity
        where TNavigation : IUser
    {
        DateTime CreatedAt { get; set; }
        Guid CreatedById { get; set; }
        TNavigation CreatedBy { get; set; }
        
        void ICreationAuditedEntity.SetCreation(IUser user)
        {
            if (user is TNavigation navigation)
            {
                CreatedAt = DateTime.Now;
                CreatedBy = navigation;
            }
            else
            {
                throw new InvalidOperationException($"A user of type {user.GetType()} can not be set as a creator for {GetType()}.");
            }
        }
    }

    public interface ICreationAuditedEntity : IEntity
    {
        void SetCreation(IUser user);
    }
}