using ESchool.Libs.Interface.Query;

namespace ESchool.ClassRegister.Interface.Features.Users.Teachers
{
    public class TeacherListQuery : PagedListQuery<UserRoleListResponse>
    {
        public string SearchText { get; set; }
    }
}