using System;
using ESchool.Libs.Interface.Query;
using ESchool.Libs.Interface.Response.Common;

namespace ESchool.ClassRegister.Interface.Features.Classes
{
    public class ClassStudentListQuery : PagedListQuery<UserRoleListResponse>
    {
        public string SearchText { get; set; }
        public Guid ClassId { get; set; }
    }
}