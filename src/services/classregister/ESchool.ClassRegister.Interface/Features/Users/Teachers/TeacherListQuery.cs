using System.Collections.Generic;
using ESchool.Libs.Interface.Response.Common;
using MediatR;

namespace ESchool.ClassRegister.Interface.Features.Users.Teachers
{
    public class TeacherListQuery : IRequest<List<UserRoleListResponse>>
    {
        public string SearchText { get; set; }
    }
}