using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using EducationAdmin.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.Options.Dto
{
    [AutoMapTo(typeof(Option))]
    public class EditOptionDto : FullAuditedEntityDto<long>
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public bool IsActive { get; set; }
    }
}
