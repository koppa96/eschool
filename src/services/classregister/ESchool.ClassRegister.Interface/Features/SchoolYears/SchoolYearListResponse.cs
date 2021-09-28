using System;

namespace ESchool.ClassRegister.Interface.Features.SchoolYears
{
    public class SchoolYearListResponse
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public SchoolYearStatus Type { get; set; }
    }
}