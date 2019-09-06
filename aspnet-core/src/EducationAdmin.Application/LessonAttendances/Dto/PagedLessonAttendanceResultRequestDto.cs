using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.LessonAttendances.Dto
{
    public class PagedLessonAttendanceResultRequestDto : PagedResultRequestDto
    {
        public long? LessonId { get; set; }
        public long? OrderId { get; set; }
    }
}
