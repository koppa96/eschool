using System;

namespace ESchool.HomeAssignments.Application.Features.HomeworkSolutions.Common
{
    public class HomeworkSolutionResponse
    {
        public Guid Id { get; set; }
        public DateTime? TurnInDate { get; set; }
        
        public class FileResponse
        {
            public Guid Id { get; set; }
            public string FileName { get; set; }
        }
    }
}