using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using EducationAdmin.Sales;
using EducationAdmin.Sales.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.Records.Dto
{
    [AutoMapTo(typeof(Record))]
    [AutoMapFrom(typeof(Record))]
    public class RecordDto : FullAuditedEntityDto<long>
    {
        public string Category { get; set; }

        public string Progress { get; set; }

        public DateTime? Date { get; set; }

        public string Content { get; set; }

        public long StudentId { get; set; }


        public StudentDto Student { get; set; }

        public long SalesmanId { get; set; }
        public string SalesmanName { get; set; }
        public int? TenantId { get; set; }

    }
}
