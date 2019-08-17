using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.Lessons.Dto
{
   public  class PagedLessonResultRequestDto:PagedResultRequestDto
    {
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public long? StudentId { get; set; }

        public long ? TeacherId { get; set; }
    }
}
