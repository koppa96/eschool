using System;
using ESchool.Libs.Interface.Query;
using ESchool.Libs.Interface.Response.Common;

namespace ESchool.ClassRegister.Interface.Features.SubjectManagement.SubjectTeachers
{
    public class SubjectTeacherListQuery : PagedListQuery<UserRoleListResponse>
    {
        public Guid SubjectId { get; set; }
    }
}