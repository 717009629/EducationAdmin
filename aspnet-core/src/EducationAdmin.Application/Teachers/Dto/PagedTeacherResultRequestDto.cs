using Abp.Application.Services.Dto;
using System;

namespace EducationAdmin.Teachers.Dto
{
    //custom PagedResultRequestDto
    public class PagedTeacherResultRequestDto : PagedResultRequestDto
    {
        public string Name { get; set; }
    }
}
