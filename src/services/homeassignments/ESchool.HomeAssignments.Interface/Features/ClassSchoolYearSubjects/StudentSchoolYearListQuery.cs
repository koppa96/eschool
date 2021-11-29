using System.Collections.Generic;
using MediatR;

namespace ESchool.HomeAssignments.Interface.Features.ClassSchoolYearSubjects
{
    public class StudentSchoolYearListQuery : IRequest<List<ClassRegisterItemResponse>>
    {
    }
}