using System;
using System.Collections.Generic;
using ESchool.ClassRegister.Application.Features.Classes.Common;

namespace ESchool.ClassRegister.Application.Features.SchoolYears.Common
{
    public class SchoolYearDetailsResponse
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public List<ClassListResponse> Classes { get; set; }
    }
}