using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.Education
{
    public class Course : FullAuditedEntity<long>, IMayHaveTenant
    {

        public string LargeCategory { get; set; }

        public string SubCategory { get; set; }

        public string Name { get; set; }
        
        public decimal Price { get; set; }
        public int? TenantId { get; set; }

        
    }
}
