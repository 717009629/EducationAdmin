using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.TimePeriods.Dto
{
    public class PagedTimePeriodResultRequestDto : PagedResultRequestDto
    {
        public bool? IsActive { get; set; }
    }
}
