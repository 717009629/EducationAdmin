using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using EducationAdmin.Sales;
using EducationAdmin.Students;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.Education
{
    public class LessonAttendance : FullAuditedEntity<long>, IMustHaveTenant
    {
        public long OrderId { get; set; }

        public Order Order { get; set; }

        public long LessonId { get; set; }

        public Lesson Lesson { get; set; }

        public bool Attended { get; set; }

        public int TenantId { get; set; }

        public override long Id { get => base.Id; set => base.Id = value; }
    }
}
