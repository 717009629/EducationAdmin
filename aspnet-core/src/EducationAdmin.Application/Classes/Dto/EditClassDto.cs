using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using EducationAdmin.Authorization.Users;
using EducationAdmin.Education;
using EducationAdmin.Lessons.Dto;
using EducationAdmin.Orders.Dto;
using EducationAdmin.Teachers.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.Classes.Dto
{
    [AutoMapTo(typeof(Class))]
    public class EditClassDto : FullAuditedEntityDto<long>
    {
        public string Name { get; set; }



        public long CourseId { get; set; }



        public long TeacherId { get; set; }
    }
}
