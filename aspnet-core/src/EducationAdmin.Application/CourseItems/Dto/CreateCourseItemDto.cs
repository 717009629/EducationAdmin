using Abp.AutoMapper;
using EducationAdmin.Students;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.CourseItems.Dto
{
    [AutoMapTo(typeof(CourseItem))]
    public class CreateCourseItemDto
    {
        public long OrderId { get; set; }

        public long CourseId { get; set; }
        public string CourseName { get; set; }

        public int Count { get; set; }

        public decimal FullMoney { get; set; }

        public string Note { get; set; }
    }
}
