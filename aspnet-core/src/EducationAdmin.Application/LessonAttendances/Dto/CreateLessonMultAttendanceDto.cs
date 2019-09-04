using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using EducationAdmin.Education;
using EducationAdmin.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.LessonAttendances.Dto
{
    public class CreateMultLessonAttendanceDto
    {



        public long LessonId { get; set; }

        public List<long> OrderIds { get; set; }

    }
}
