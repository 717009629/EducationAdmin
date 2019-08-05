using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.Students.Dto
{
    public class PagedStudentResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}
