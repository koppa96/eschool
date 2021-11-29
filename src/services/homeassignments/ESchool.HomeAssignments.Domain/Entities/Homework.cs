using System;
using System.Collections.Generic;
using ESchool.HomeAssignments.Domain.Entities.ClassRegisterData;
using ESchool.HomeAssignments.Domain.Entities.Users;
using ESchool.Libs.Domain.Interfaces;
using ESchool.Libs.Domain.Interfaces.Audit;

namespace ESchool.HomeAssignments.Domain.Entities
{
    public class Homework : IFullAudited<HomeAssignmentsUser, HomeAssignmentsUserRole>, IEntity
    {
        public Guid Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        
        public DateTime Deadline { get; set; }
        public bool Optional { get; set; }

        public Guid LessonId { get; set; }
        public virtual Lesson Lesson { get; set; }

        public DateTime CreatedAt { get; set; }
        public Guid? CreatedById { get; set; }
        public virtual HomeAssignmentsUser CreatedBy { get; set; }

        public DateTime? LastModifiedAt { get; set; }
        public Guid? LastModifiedById { get; set; }
        public virtual HomeAssignmentsUser LastModifiedBy { get; set; }

        public virtual ICollection<HomeworkSolution> Solutions { get; set; }
    }
}
