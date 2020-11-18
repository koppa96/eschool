namespace ESchool.Libs.Domain
{
    public static class Constants
    {
        public static class ClaimTypes
        {
            public const string GlobalRole = "global_role";
            public const string TenantRoles = "tenant_roles";

            public static string TenantRoleId(int index) => TenantRoles + $":{index}:id";
            public static string TenantId(int index) => TenantRoles + $":{index}:tenantId";
            public static string TenantRole(int index) => TenantRoles + $":{index}:tenantRole";
        }
    }
}