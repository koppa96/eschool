using System;

namespace ESchool.ClassRegister.Interface.Features.ClassTypes
{
    public class ClassTypeDetailsResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}