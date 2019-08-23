using Abp.AutoMapper;
using EducationAdmin.Education;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.Courses.Dto
{
    [AutoMapTo(typeof(Course))]
 public   class CreateCourseDto
    {
        public string Category { get; set; }


        public string Name { get; set; }

        public decimal Price { get; set; }
        public ClassType ClassType { get; set; }

        public string Note { get; set; }
    }
}
