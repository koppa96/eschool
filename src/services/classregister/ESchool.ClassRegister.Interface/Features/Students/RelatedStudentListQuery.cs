using System.Collections.Generic;
using ESchool.Libs.Interface.Response.Common;
using MediatR;

namespace ESchool.ClassRegister.Interface.Features.Students
{
    public class RelatedStudentListQuery : IRequest<List<UserRoleListResponse>>
    {
    }
}