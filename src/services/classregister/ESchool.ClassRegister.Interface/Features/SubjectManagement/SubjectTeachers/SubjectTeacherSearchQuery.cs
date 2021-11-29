using System;
using System.Collections.Generic;
using ESchool.Libs.Interface.Response.Common;
using MediatR;

namespace ESchool.ClassRegister.Interface.Features.SubjectManagement.SubjectTeachers
{
    public class SubjectTeacherSearchQuery : IRequest<List<UserRoleListResponse>>
    {
        public Guid SubjectId { get; set; }
        public string SearchText { get; set; }
    }
}