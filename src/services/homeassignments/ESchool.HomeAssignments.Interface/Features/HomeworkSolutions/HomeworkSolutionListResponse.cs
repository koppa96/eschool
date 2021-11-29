using System;
using ESchool.Libs.Interface.Response.Common;

namespace ESchool.HomeAssignments.Interface.Features.HomeworkSolutions
{
    public class HomeworkSolutionListResponse
    {
        public Guid Id { get; set; }
        public UserRoleListResponse Student { get; set; }
        public DateTime? TurnInDate { get; set; }
        public bool Reviewed { get; set; }
    }
}