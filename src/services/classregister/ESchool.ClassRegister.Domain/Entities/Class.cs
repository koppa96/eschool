﻿using ESchool.Libs.Domain.Interfaces;
using System;
using System.Collections.Generic;
using ESchool.ClassRegister.Domain.Entities.Users;

namespace ESchool.ClassRegister.Domain.Entities
{
    public class Class : IMultiTenantEntity
    {
        public Guid Id { get; set; }

        public bool DidFinish { get; set; }
        public int Grade => ClassType.StartingGrade + ClassSchoolYears.Count - 1;

        public Guid? HeadTeacherId { get; set; }
        public virtual Teacher HeadTeacher { get; set; }

        public Guid ClassTypeId { get; set; }
        public virtual ClassType ClassType { get; set; }

        public Guid TenantId { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<ClassSchoolYear> ClassSchoolYears { get; set; }
    }
}
