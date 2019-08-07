using Abp.AutoMapper;
using EducationAdmin.Students;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.Records.Dto
{
    [AutoMapTo(typeof(Record))]
    public class CreateRecordDto
    {
        public string Category { get; set; }

        public string Progress { get; set; }

        public DateTime? Date { get; set; }

        public string Content { get; set; }

        public long StudentId { get; set; }

        public long SalesmanId { get; set; }


    }
}
