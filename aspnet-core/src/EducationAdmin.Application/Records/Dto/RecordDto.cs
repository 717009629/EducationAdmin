using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using EducationAdmin.Students;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.Records.Dto
{
    [AutoMapTo(typeof(Record))]
    class RecordDto:FullAuditedEntity<Guid>
    {
        public string Category { get; set; }

        public string Progess { get; set; }

        public DateTime? Date { get; set; }

        public string Content { get; set; }

        public Guid StudentId { get; set; }

        public string StudentName { get; set; }

        public long SalesmanId { get; set; }
        public string  SalesmanName { get; set; }
        public int? TenantId { get; set; }

    }
}
