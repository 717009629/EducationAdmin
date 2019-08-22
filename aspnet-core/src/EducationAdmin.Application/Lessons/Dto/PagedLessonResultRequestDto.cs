using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.Lessons.Dto
{
   public  class PagedLessonResultRequestDto:PagedResultRequestDto
    {
        public DateTime? Start { get; set; }

        public DateTime? End { get; set; }

        public long? StudentId { get; set; }

        public long ? TeacherId { get; set; }

        public long? ClassId { get; set; }
    }
}
