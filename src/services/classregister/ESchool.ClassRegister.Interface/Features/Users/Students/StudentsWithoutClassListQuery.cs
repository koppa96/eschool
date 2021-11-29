using ESchool.Libs.Interface.Query;
using ESchool.Libs.Interface.Response.Common;

namespace ESchool.ClassRegister.Interface.Features.Users.Students
{
    public class StudentsWithoutClassListQuery : PagedListQuery<UserRoleListResponse>
    {
        public string SearchText { get; set; }
    }
}