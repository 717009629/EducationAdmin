using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.Subjects.Dto
{
    public class PagedSubjectResultRequestDto : PagedResultRequestDto
    {
        public bool? IsActive { get; set; }
    }
}
