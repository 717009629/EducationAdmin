using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.Settings
{
    public class Subject:FullAuditedEntity<long>
    {
        public string Name { get; set; }

        public bool IsActive { get; set; }


    }
}
