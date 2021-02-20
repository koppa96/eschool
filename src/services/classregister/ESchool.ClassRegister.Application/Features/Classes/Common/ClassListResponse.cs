using System;
using ESchool.ClassRegister.Application.Features.ClassTypes.Common;

namespace ESchool.ClassRegister.Application.Features.Classes.Common
{
    public class ClassListResponse
    {
        public Guid Id { get; set; }
        public int Grade { get; set; }
        public ClassTypeListResponse ClassType { get; set; }
    }
}