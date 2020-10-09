using ESchool.ClassRegister.Domain.Entities.SubjectManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESchool.ClassRegister.Domain.Entities.Grading
{
    public class SmallGradesPolicy
    {
        public Guid Id { get; set; }

        public Guid GroupId { get; set; }
        public virtual Group Group { get; set; }

        public int NumberOfGradesToAverage { get; set; }
    }
}
