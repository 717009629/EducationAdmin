using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.Settings
{
    public class Option:FullAuditedEntity<long>
    {
        public string Category { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }

        public double? order { get; set; }
    }
}
