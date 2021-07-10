using System;
using System.Collections.Generic;
using ESchool.ClassRegister.Interface.Features.Users;

namespace ESchool.ClassRegister.Interface.Features.Subjects
{
    public class SubjectDetailsResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<UserRoleListResponse> Teachers { get; set; }
    }
}