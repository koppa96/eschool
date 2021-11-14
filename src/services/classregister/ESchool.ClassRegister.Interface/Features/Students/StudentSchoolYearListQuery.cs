using System;
using System.Collections.Generic;
using ESchool.ClassRegister.Interface.Features.SchoolYears;
using MediatR;

namespace ESchool.ClassRegister.Interface.Features.Students
{
    public class StudentSchoolYearListQuery : IRequest<List<SchoolYearListResponse>>
    {
        public Guid StudentId { get; set; }        
    }
}