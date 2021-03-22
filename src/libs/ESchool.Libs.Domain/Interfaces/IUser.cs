using System;

namespace ESchool.Libs.Domain.Interfaces
{
    public interface IUser : IEntity
    {
        public Guid UserId { get; set; }
    }
}