﻿using System;
using ESchool.ClassRegister.Application.Features.ClassTypes.Common;
using ESchool.ClassRegister.Application.Features.SchoolYears;

namespace ESchool.ClassRegister.Application.Features.Classes.Common
{
    public class ClassListResponse
    {
        public Guid Id { get; set; }
        public int Grade { get; set; }
        public bool DidFinish { get; set; }
        public SchoolYearListResponse FinishingSchoolYear { get; set; }
        public ClassTypeListResponse ClassType { get; set; }
    }
}