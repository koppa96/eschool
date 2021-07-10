using System;
using MediatR;

namespace ESchool.ClassRegister.Interface.Features.ClassSchoolYears
{
    public class ClassSchoolYearCreateCommand : IRequest
    {
        public Guid ClassId { get; set; }
        public Guid SchoolYearId { get; set; }
    }
}