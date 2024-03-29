﻿using ESchool.ClassRegister.Domain.Entities.SubjectManagement;
using ESchool.ClassRegister.Domain.Entities.Users;
using System;
using ESchool.ClassRegister.SharedDomain.Enums;
using ESchool.Libs.Domain.Interfaces;

namespace ESchool.ClassRegister.Domain.Entities.Grading
{
    public class Grade : IEntity
    {
        public Guid Id { get; set; }

        public GradeValue Value { get; set; }
        public string Description { get; set; }

        public DateTime WrittenIn { get; set; }

        public Guid KindId { get; set; }
        public virtual GradeKind Kind { get; set; }

        public Guid? StudentId { get; set; }
        public virtual Student Student { get; set; }

        public Guid ClassSchoolYearSubjectId { get; set; }
        public virtual ClassSchoolYearSubject ClassSchoolYearSubject { get; set; }

        public Guid? TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}
