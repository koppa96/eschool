using ESchool.Libs.Domain.Interfaces;
using ESchool.Libs.Domain.MultiTenancy.Entities;

namespace ESchool.Testing.Domain.Entities.Users
{
    public class TestingUserRole : UserRoleBase<TestingUser, TestingUserRole>, ISoftDelete
    {
        public bool IsDeleted { get; set; }
    }
}