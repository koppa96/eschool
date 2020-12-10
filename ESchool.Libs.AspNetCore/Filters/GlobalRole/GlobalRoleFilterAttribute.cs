using System.Linq;
using ESchool.Libs.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.Libs.AspNetCore.Filters.GlobalRole
{
    public class GlobalRoleFilterAttribute : TypeFilterAttribute
    {
        public GlobalRoleFilterAttribute(params GlobalRoleType[] globalRoleTypes) : base(typeof(GlobalRoleFilter))
        {
            Arguments = new object[] { globalRoleTypes.AsEnumerable() };
        }
    }
}