using System;
using ESchool.ClassRegister.SharedDomain.Enums;

namespace ESchool.ClassRegister.Interface.Features.SchoolYears
{
    public class SchoolYearListResponse
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public SchoolYearStatus Status { get; set; }
    }
}