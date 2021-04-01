using System;
using System.Collections.Generic;
using ESchool.ClassRegister.Application.Features.Users.Common;

namespace ESchool.ClassRegister.Application.Features.Subjects.Common
{
    public class SubjectDetailsResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<UserRoleListResponse> Teachers { get; set; }
    }
}