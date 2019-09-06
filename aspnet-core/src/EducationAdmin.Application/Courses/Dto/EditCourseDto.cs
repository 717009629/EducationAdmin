using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using EducationAdmin.Education;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.Courses.Dto
{
    [AutoMapTo(typeof(Course))]
  public  class EditCourseDto:FullAuditedEntityDto<long>
    {
        public string Category { get; set; }


        public string Name { get; set; }

        public decimal Price { get; set; }


        public string Note { get; set; }

        public int Count { get; set; }
    }
}
