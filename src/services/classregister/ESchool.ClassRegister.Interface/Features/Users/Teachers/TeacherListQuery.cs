using ESchool.Libs.Interface.Query;
using ESchool.Libs.Interface.Response.Common;

namespace ESchool.ClassRegister.Interface.Features.Users.Teachers
{
    public class TeacherListQuery : PagedListQuery<UserRoleListResponse>
    {
        public string SearchText { get; set; }
    }
}