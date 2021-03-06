using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.Settings
{
    public class Subject:FullAuditedEntity<long>,IMustHaveTenant
    {
        public string Name { get; set; }

        public bool IsActive { get; set; }


        public double? order { get; set; }
        public int TenantId { get ; set ; }
    }
}
