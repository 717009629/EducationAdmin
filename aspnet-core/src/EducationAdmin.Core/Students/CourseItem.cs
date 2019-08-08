using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using EducationAdmin.Education;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.Students
{
    public class CourseItem : FullAuditedEntity<long>, IMayHaveTenant
    {

        public long OrderId { get; set; }
        public Order Order { get; set; }

        public long CourseId { get; set; }
        public Course Course { get; set; }

        public int Count { get; set; }

        public decimal FullMoney { get; set; }

        public string Note { get; set; }

        public int? TenantId { get; set; }
    }
}
