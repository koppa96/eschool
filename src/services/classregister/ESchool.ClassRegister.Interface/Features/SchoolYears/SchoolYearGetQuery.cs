using System;
using MediatR;

namespace ESchool.ClassRegister.Interface.Features.SchoolYears
{
    public class SchoolYearGetQuery : IRequest<SchoolYearDetailsResponse>
    {
        public Guid Id { get; set; }
    }
}