using System;
using System.Collections.Generic;
using System.Text;

namespace ESchool.TenantManagement.Domain
{
    public class Tenant
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }
        public string OfficialEmailAddress { get; set; }
        public string OMIdentifier { get; set; }
        public string HeadMaster { get; set; }
    }
}
