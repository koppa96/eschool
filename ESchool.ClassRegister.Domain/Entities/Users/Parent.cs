using System.Collections.Generic;

namespace ESchool.ClassRegister.Domain.Entities.Users
{
    public class Parent : UserBase
    {
        public virtual ICollection<StudentParent> StudentParents { get; set; }
    }
}
