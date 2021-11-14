using ESchool.Libs.Interface.Query;

namespace ESchool.ClassRegister.Interface.Features.Classes
{
    public class ClassListQuery : PagedListQuery<ClassListResponse>
    {
        public bool IncludeFinishedClasses { get; set; }
    }
}