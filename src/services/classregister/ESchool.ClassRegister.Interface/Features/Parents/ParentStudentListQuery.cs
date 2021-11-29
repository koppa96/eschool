using System;
using System.Collections.Generic;
using ESchool.Libs.Interface.Response.Common;
using MediatR;

namespace ESchool.ClassRegister.Interface.Features.Parents
{
    public class ParentStudentListQuery : IRequest<List<UserRoleListResponse>>
    {
        public Guid ParentId { get; set; }
    }
}