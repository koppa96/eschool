using System;
using System.Collections.Generic;
using System.Text;

namespace ESchool.ClassRegister.Domain.Entities.Users
{
    public class Parent : UserBase
    {
        public virtual ICollection<StudentParent> StudentParents { get; set; }
    }
}
