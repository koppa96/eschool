using System;

namespace ESchool.Testing.Domain.Entities.ClassRegisterData
{
    public class Teacher
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Guid UserId { get; set; }
    }
}