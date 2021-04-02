using System;
using ESchool.Testing.Domain.Entities.Users;

namespace ESchool.Testing.Domain.Entities
{
    public class StudentTest
    {
        public Guid Id { get; set; }

        public Guid StudentId { get; set; }
        public virtual Student Student { get; set; }

        public Guid TestId { get; set; }
        public virtual Test Test { get; set; }

        public Guid? TestAnswerId { get; set; }
        public virtual TestAnswer TestAnswer { get; set; }
    }
}