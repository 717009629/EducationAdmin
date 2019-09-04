using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using EducationAdmin.Education;
using EducationAdmin.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.LessonAttendances.Dto
{
    [AutoMapTo(typeof(LessonAttendance))]
    public class EditLessonAttendanceDto : FullAuditedEntityDto<long>
    {
        public long OrderId { get; set; }


        public long LessonId { get; set; }

        public bool Attended { get; set; }
    }
}
