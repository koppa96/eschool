using System;
using ESchool.ClassRegister.SharedDomain.Enums;

namespace ESchool.ClassRegister.Interface.Features.SchoolYears
{
    public class SchoolYearEditCommand
    {
        public string DisplayName { get; set; }
        public SchoolYearStatus Status { get; set; }
        public DateTime StartsAt { get; set; }
        public DateTime EndOfFirstHalf { get; set; }
        public DateTime EndsAt { get; set; }
    }
}