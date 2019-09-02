using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.Options.Dto
{
    public class PagedOptionResultRequestDto : PagedResultRequestDto
    {
        public bool? IsActive { get; set; }
    }
}
