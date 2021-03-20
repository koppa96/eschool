using System;

namespace ESchool.HomeAssignments.Domain.Entities
{
    public class File
    {
        public Guid Id { get; set; }

        public string FileName { get; set; }

        public Guid HomeWorkSolutionId { get; set; }
        public virtual HomeworkSolution HomeworkSolution { get; set; }
    }
}