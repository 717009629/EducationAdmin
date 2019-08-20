using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using EducationAdmin.Authorization.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.Teachers.Dto
{
    [AutoMapFrom(typeof(User))]
    public  class TeacherDto : FullAuditedEntityDto<long>
    {
        public string UserName { get; set; }


        public string Name { get; set; }


        public string EmailAddress { get; set; }


        public DateTime? Birthday { get; set; }

        public bool? Sex { get; set; }



        public string Phone { get; set; }


        public bool IsActive { get; set; }

        public string FullName { get; set; }

        public DateTime? LastLoginTime { get; set; }

    }
}
