using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using EducationAdmin.Authorization.Users;
using EducationAdmin.Students;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EducationAdmin.Sales
{
    public class Record : FullAuditedEntity<long>, IMustHaveTenant
    {

        public string State { get; set; }

        public DateTime? Date { get; set; }

        public string Content { get; set; }

        public long CustomerId { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public Customer Customer { get; set; }

        public long SalesmanId { get; set; }
        [ForeignKey(nameof(SalesmanId))]
        public User Salesman { get; set; }
        public int TenantId { get; set; }

    }
}
