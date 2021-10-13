using System;
using System.Collections.Generic;
using ESchool.HomeAssignments.Interface.Features.HomeworkReviews;
using ESchool.Libs.Interface.Response.Common;

namespace ESchool.HomeAssignments.Interface.Features.HomeworkSolutions
{
    public class HomeworkSolutionResponse
    {
        public Guid Id { get; set; }
        public DateTime? TurnInDate { get; set; }
        public HomeworkReviewResponse Review { get; set; }
        public UserRoleListResponse Student { get; set; }
        public List<FileResponse> Files { get; set; }
        
        public class FileResponse
        {
            public Guid Id { get; set; }
            public string FileName { get; set; }
        }
    }
}