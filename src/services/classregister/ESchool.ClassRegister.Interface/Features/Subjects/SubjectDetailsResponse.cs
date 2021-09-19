using System;
using System.Collections.Generic;
using ESchool.ClassRegister.Interface.Features.Users;
using ESchool.Libs.Interface.Response.Common;

namespace ESchool.ClassRegister.Interface.Features.Subjects
{
    public class SubjectDetailsResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}