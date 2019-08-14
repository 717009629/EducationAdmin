using Abp.AutoMapper;
using EducationAdmin.Sales;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.Records.Dto
{
    [AutoMapTo(typeof(Record))]
    public class CreateRecordDto
    {

        public CustomerState State { get; set; }

        public DateTime? Date { get; set; }

        public string Content { get; set; }

        public long CustomerId { get; set; }

        public long SalesmanId { get; set; }


    }
}
