using System;

namespace ESchool.Libs.Domain.MultiTenancy.Entities
{
    public class Tenant
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string OmIdentifier { get; set; }
        public string DbConnectionString { get; set; }
    }
}