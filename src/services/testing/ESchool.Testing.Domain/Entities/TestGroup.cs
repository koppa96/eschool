using System;
using ESchool.Testing.Domain.Entities.ClassRegisterData;

namespace ESchool.Testing.Domain.Entities
{
    public class TestGroup
    {
        public Guid Id { get; set; }
        
        public Guid GroupId { get; set; }
        public virtual Group Group { get; set; }

        public Guid TestId { get; set; }
        public virtual Test Test { get; set; }
    }
}