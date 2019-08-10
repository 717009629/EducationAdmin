using Abp.Domain.Entities.Auditing;
using EducationAdmin.Authorization.Users;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EducationAdmin.Students
{
    public class Contract:FullAuditedEntity<long>
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal FullMoney { get; set; }

        public ContractState State { get; set; }

        public string Note { get; set; }

        public string AuditedReson { get; set; }

        public long StudentId { get; set; }

        public Student Student { get; set; }

        public long? AuditorId { get; set; }

        public User Auditor { get; set; }

        public long SalesmanId { get; set; }

        public User Salesman { get; set; }

        [ForeignKey(nameof(Id))]
        public Order Order { get; set; }
    }
    public enum ContractState
    {
        
        Created=0,
        WaitAudite=1,
        Audited=2,
        Reject=3,
        Obsolete=4
    }
}
