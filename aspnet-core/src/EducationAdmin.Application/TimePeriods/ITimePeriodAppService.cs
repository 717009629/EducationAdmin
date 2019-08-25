using Abp.Application.Services;
using Abp.Application.Services.Dto;
using EducationAdmin.TimePeriods.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.TimePeriods
{
    public interface ITimePeriodAppService:IAsyncCrudAppService<TimePeriodDto,long,PagedResultRequestDto, CreateTimePeriodDto, TimePeriodDto>
    {
    }
}
