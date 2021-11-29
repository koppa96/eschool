using System;
using MediatR;

namespace ESchool.ClassRegister.Interface.Features.SchoolYears
{
    public class SchoolYearCreateCommand : IRequest<SchoolYearDetailsResponse>
    {
        public string DisplayName { get; set; }
        public DateTime StartsAt { get; set; }
        public DateTime EndOfFirstHalf { get; set; }
        public DateTime EndsAt { get; set; }
    }
}