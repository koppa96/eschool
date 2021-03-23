using ESchool.Libs.Domain.Interfaces;
using System;
using ESchool.Libs.Domain.MultiTenancy.Entities;

namespace ESchool.HomeAssignments.Domain.Entities.Users
{
    public class HomeAssignmentsUser : UserBase<HomeAssignmentsUser, HomeAssignmentsUserRole>, ISoftDelete
    {
        public bool IsDeleted { get; set; }
    }
}
