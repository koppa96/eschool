using System;

namespace ESchool.ClassRegister.Domain.Entities.MultiTenancy
{
    public class Tenant
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DbConnectionString { get; set; }
    }
}