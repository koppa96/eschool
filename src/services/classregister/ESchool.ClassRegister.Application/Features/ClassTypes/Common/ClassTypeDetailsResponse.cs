using System;

namespace ESchool.ClassRegister.Application.Features.ClassTypes.Common
{
    public class ClassTypeDetailsResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}