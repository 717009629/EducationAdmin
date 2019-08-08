using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.Records.Dto
{
    public class PagedRecordResultRequestDto : PagedResultRequestDto
    {
        public int? StudentId { get; set; }
    }
}
