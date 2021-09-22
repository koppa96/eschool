using System;
using System.Collections.Generic;
using ESchool.ClassRegister.Interface.Features.Classes;

namespace ESchool.ClassRegister.Interface.Features.SchoolYears
{
    public class SchoolYearDetailsResponse
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        
        public DateTime StartsAt { get; set; }
        public DateTime EndOfFirstHalf { get; set; }
        public DateTime EndsAt { get; set; }
    }
}