using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.Settings
{
    public class TimePeriod : FullAuditedEntity<long>, IMustHaveTenant
    {
        public TimeSpan Start { get; set; }

        public TimeSpan End { get; set; }
        public int TenantId { get; set ; }

    }
}
