using Abp.Application.Services.Dto;
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
    public class EditLessionDto:FullAuditedEntityDto<long>
    {

        public DateTime LessonDate { get; set; }


        public long ClassId { get; set; }


        public int LessonIndex { get; set; }

        public bool IsFinish { get; set; }

        public long TeacherId { get; set; }


        public string Course { get; set; }
    }
}
