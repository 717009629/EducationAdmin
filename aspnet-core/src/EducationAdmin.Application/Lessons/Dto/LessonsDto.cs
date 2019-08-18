using Abp.Application.Services.Dto;
using Abp.AutoMapper;
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
    public class LessonsDto:FullAuditedEntityDto<long>
    {
        public DateTime LessonDate { get; set; }


        public long StudentId { get; set; }

        public StudentDto Student { get; set; }


        public long OrderId { get; set; }
        public OrderDto Order { get; set; }

        public bool IsFinish { get; set; }

        public long TeacherId { get; set; }

        public UserDto Teacher { get; set; }

        public string Course { get; set; }
    }
}
