using System;
using System.Collections.Generic;
using ESchool.Libs.Domain.Enums;

namespace ESchool.ClassRegister.Interface.Features.Users
{
    public class ClassRegisterUserListResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        
        public List<TenantRoleType> Roles { get; set; }
    }

}