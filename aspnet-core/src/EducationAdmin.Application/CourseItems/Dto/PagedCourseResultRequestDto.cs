using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.CourseItems.Dto
{
    public class PagedCourseResultRequestDto:PagedResultRequestDto
    {
        public long? OrderId { get; set; }
    }
}
