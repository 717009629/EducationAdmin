using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using EducationAdmin.Classes.Dto;
using EducationAdmin.Education;
using EducationAdmin.Orders.Dto;
using EducationAdmin.Sales.Dto;
using EducationAdmin.Students;
using EducationAdmin.Users.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.Lessons.Dto
{
    [AutoMapTo(typeof(Lesson))]
    [AutoMapFrom(typeof(Lesson))]
    public class LessonDto : FullAuditedEntityDto<long>
    {
        public DateTime LessonDate { get; set; }


        public long ClassId { get; set; }

        public ClassDto Class { get; set; }

        public int LessonIndex { get; set; }


        public bool IsFinish { get; set; }

        public long TeacherId { get; set; }

        public UserDto Teacher { get; set; }

        public string Course { get; set; }
    }
}
