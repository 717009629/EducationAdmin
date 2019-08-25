using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using EducationAdmin.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.TimePeriods.Dto
{
    [AutoMapFrom(typeof(TimePeriod))]
    [AutoMapTo(typeof(TimePeriod))]
    public class TimePeriodDto:FullAuditedEntityDto<long>   
    {
        public TimeSpan Start { get; set; }

        public TimeSpan End { get; set; }
    }
}
