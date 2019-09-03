using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using EducationAdmin.Education;
using EducationAdmin.Lessons.Dto;
using EducationAdmin.Sales.Dto;
using EducationAdmin.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.LessonAttendances.Dto
{
    [AutoMapFrom(typeof(LessonAttendance))]
    [AutoMapTo(typeof(LessonAttendance))]
    public class LessonAttendanceDto : FullAuditedEntityDto <long>
    {
        public long StudentId { get; set; }

        public StudentDto Student { get; set; }

        public long LessonId { get; set; }

        public LessonDto Lesson { get; set; }

        public bool Attended { get; set; }
    }
}
