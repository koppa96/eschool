using System;

namespace ESchool.ClassRegister.Interface.Features.SchoolYears
{
    public class SchoolYearEditCommand
    {
        public string DisplayName { get; set; }
        public DateTime StartsAt { get; set; }
        public DateTime EndOfFirstHalf { get; set; }
        public DateTime EndsAt { get; set; }
    }
}