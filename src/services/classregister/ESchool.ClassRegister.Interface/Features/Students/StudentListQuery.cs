using ESchool.Libs.Interface.Query;
using ESchool.Libs.Interface.Response.Common;

namespace ESchool.ClassRegister.Interface.Features.Students
{
    public class StudentListQuery : PagedListQuery<UserRoleListResponse>
    {
        public string SearchText { get; set; }
    }
}