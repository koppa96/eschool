using ESchool.Libs.Domain.Enums;

namespace ESchool.Libs.AspNetCore
{
    public class PolicyNames
    {
        public const string TenantAdministrator = nameof(GlobalRoleType.TenantAdministrator);
        public const string Administrator = nameof(TenantRoleType.Administrator);
        public const string Parent = nameof(TenantRoleType.Parent);
        public const string Student = nameof(TenantRoleType.Student);
        public const string Teacher = nameof(TenantRoleType.Teacher);
        public const string TeacherOrAdministrator = nameof(TeacherOrAdministrator);
        public const string TeacherOrStudent = nameof(TeacherOrStudent);
        public const string AnyRole = nameof(AnyRole);
        public const string AdministratorOrTenantAdministrator = nameof(AdministratorOrTenantAdministrator);
        public const string StudentOrParent = nameof(StudentOrParent);
    }
}