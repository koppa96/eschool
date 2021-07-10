using ESchool.Libs.Interface.Query;

namespace ESchool.ClassRegister.Interface.Features.Users.Students
{
    public class StudentsWithoutClassListQuery : PagedListQuery<UserRoleListResponse>
    {
        public string SearchText { get; set; }
    }
}