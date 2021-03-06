﻿using System;
using ESchool.ClassRegister.Domain.Enums;

namespace ESchool.ClassRegister.Api.Dtos
{
    public class ClassSchoolYearSubjectGradeCreateDto
    {
        public GradeValue Value { get; set; }
        public string Description { get; set; }
        public Guid GradeKindId { get; set; }
    }
}