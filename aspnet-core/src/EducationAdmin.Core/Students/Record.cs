using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using EducationAdmin.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EducationAdmin.Students
{
    public class Record : FullAuditedEntity<Guid>, IMayHaveTenant
    {
        public string Category { get; set; }

        public string Progess { get; set; }

        public DateTime? Date { get; set; }

        public string Content { get; set; }

        public Guid StudentId { get; set; }

        [ForeignKey(nameof(StudentId))]
        public Student Student { get; set; }

        public long SalesmanId { get; set; }
        [ForeignKey(nameof(SalesmanId))]
        public User Salesman { get; set; }
        public int? TenantId { get; set; }

    }
}
