using System;

namespace ESchool.HomeAssignments.Interface.Features.Homeworks
{
    public class HomeworkEditCommand
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Optional { get; set; }
        public DateTime Deadline { get; set; }
    }
}