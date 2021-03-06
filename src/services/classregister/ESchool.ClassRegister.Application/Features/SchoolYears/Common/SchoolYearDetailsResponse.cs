﻿using System;
using System.Collections.Generic;
using ESchool.ClassRegister.Application.Features.Classes.Common;

namespace ESchool.ClassRegister.Application.Features.SchoolYears.Common
{
    public class SchoolYearDetailsResponse
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        
        public DateTime StartsAt { get; set; }
        public DateTime EndOfFirstHalf { get; set; }
        public DateTime EndsAt { get; set; }
        
        public List<ClassListResponse> Classes { get; set; }
    }
}