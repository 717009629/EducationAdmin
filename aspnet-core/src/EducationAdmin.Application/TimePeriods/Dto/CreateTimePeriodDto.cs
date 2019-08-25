using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using EducationAdmin.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.TimePeriods.Dto
{
    [AutoMapTo(typeof(TimePeriod))]
    public class CreateTimePeriodDto
    {
        public TimeSpan Start { get; set; }

        public TimeSpan End { get; set; }

        public bool IsActive { get; set; }
    }
}
