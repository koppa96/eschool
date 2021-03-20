using ESchool.Libs.Domain.Interfaces;
using System;

namespace ESchool.HomeAssignments.Domain.Entities.Users
{
    public class UserBase : ISoftDelete
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        
        public Guid UserId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
