using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using EducationAdmin.Sales;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.CourseItems.Dto
{
    [AutoMapFrom(typeof(CourseItem))]
    [AutoMapTo(typeof(CourseItem))]
    public class CourseItemDto:FullAuditedEntityDto<long>
    {
        public long OrderId { get; set; }

        public long CourseId { get; set; }
        public string CourseName { get; set; }

        public string CourseCategory { get; set; }

        public int Count { get; set; }

        public decimal FullMoney { get; set; }

        public string Note { get; set; }


    }
}
