using System;
using ESchool.Libs.Domain.Interfaces;

namespace ESchool.ClassRegister.Domain.Entities
{
    public class ClassType : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int StartingGrade { get; set; }
    }
}