﻿using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using EducationAdmin.Education;
using EducationAdmin.Students;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.Lessons.Dto
{
    [AutoMapTo(typeof(Lesson))]
    public class EditLessionDto : FullAuditedEntityDto<long>
    {

        public DateTime LessonDate { get; set; }


        public long ClassId { get; set; }


        public int LessonNumber { get; set; }

        public bool IsFinish { get; set; }

        public long TeacherId { get; set; }


        public string Subject { get; set; }

        public long TimePeriodId { get; set; }

        public TimeSpan Start { get; set; }

        public TimeSpan End { get; set; }
    }
}
