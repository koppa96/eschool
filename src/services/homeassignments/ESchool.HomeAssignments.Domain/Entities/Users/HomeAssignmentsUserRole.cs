using ESchool.Libs.Domain.Interfaces;
using ESchool.Libs.Domain.MultiTenancy.Entities;

namespace ESchool.HomeAssignments.Domain.Entities.Users
{
    public class HomeAssignmentsUserRole : UserRoleBase<HomeAssignmentsUser, HomeAssignmentsUserRole>, ISoftDelete
    {
        public bool IsDeleted { get; set; }
    }
}