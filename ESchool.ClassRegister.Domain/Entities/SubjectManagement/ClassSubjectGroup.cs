using System;
using System.Collections.Generic;
using System.Text;

namespace ESchool.ClassRegister.Domain.Entities.SubjectManagement
{
    public class ClassSubjectGroup
    {
        public Guid Id { get; set; }

        public Guid ClassSubjectId { get; set; }
        public virtual ClassSubject ClassSubject { get; set; }

        public Guid GroupId { get; set; }
        public virtual Group Group { get; set; }
    }
}
