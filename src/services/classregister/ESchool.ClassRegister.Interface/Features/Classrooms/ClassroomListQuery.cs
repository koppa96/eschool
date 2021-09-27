using ESchool.Libs.Interface.Query;

namespace ESchool.ClassRegister.Interface.Features.Classrooms
{
    public class ClassroomListQuery : PagedListQuery<ClassroomListResponse>
    {
        public string SearchText { get; set; }
    }
}