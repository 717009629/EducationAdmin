using Abp.Application.Services.Dto;
using EducationAdmin.Education;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.Courses.Dto
{
    public class PagedCourseResultRequestDto: PagedResultRequestDto
    {
        public ClassType? ClassType { get; set; }
    }
}
