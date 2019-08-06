using Abp.AutoMapper;
using EducationAdmin.Students;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.Records.Dto
{
    [AutoMapTo(typeof(Record))]
    class CreateRecordDto
    {
        public string Category { get; set; }

        public string Progess { get; set; }

        public DateTime? Date { get; set; }

        public string Content { get; set; }

        public Guid StudentId { get; set; }


    }
}
