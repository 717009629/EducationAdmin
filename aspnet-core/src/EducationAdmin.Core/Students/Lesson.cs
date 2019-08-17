using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using EducationAdmin.Authorization.Users;
using EducationAdmin.Sales;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.Students
{
    public class Lesson : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        public DateTime LessionDate { get; set; }


        public long StudentId { get; set; }

        public Student Student { get; set; }


        public long OrderId { get; set; }
        public Order Order { get; set; }

        public bool IsFinish { get; set; }

        public long TeacherId { get; set; }

        public User Teacher { get; set; }

        public string Course { get; set; }
    }
}
