using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using EducationAdmin.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.Subjects.Dto
{
    [AutoMapFrom(typeof(Subject))]
    [AutoMapTo(typeof(Subject))]
    public class SubjectDto : FullAuditedEntityDto <long>
    {
        public string Name { get; set; }

        public bool IsActive { get; set; }
    }
}
