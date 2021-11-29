using System.Collections.Generic;
using MediatR;

namespace ESchool.HomeAssignments.Interface.Features.ClassSchoolYearSubjects
{
    public class TeacherSchoolYearListQuery : IRequest<List<ClassRegisterItemResponse>>
    {
    }
}