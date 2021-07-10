using System;
using ESchool.Libs.Interface.Query;

namespace ESchool.HomeAssignments.Interface.Features.HomeworkSolutions
{
    public class HomeworkSolutionListQuery : PagedListQuery<HomeworkSolutionListResponse>
    {
        public Guid HomeworkId { get; set; }
    }
}