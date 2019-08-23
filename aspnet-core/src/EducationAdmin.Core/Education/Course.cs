using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.Education
{
    public class Course : FullAuditedEntity<long>, IMustHaveTenant
    {


        public string Category { get; set; }

        public string Name { get; set; }

        public ClassType ClassType { get; set; }
        
        public decimal Price { get; set; }

        public int TenantId { get; set; }

        public string Note { get; set; }
    }
}
