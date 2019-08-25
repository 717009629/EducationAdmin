using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using EducationAdmin.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.TimePeriods.Dto
{
    [AutoMapTo(typeof(TimePeriod))]
    public class EditTimePeriodDto
    {

        public bool IsActive { get; set; }
    }
}
