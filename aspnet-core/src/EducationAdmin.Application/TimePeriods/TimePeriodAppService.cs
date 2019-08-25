using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using EducationAdmin.Settings;
using EducationAdmin.TimePeriods.Dto;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace EducationAdmin.TimePeriods
{
    public class TimePeriodAppService:AsyncCrudAppService<TimePeriod,TimePeriodDto,long,PagedResultRequestDto,CreateTimePeriodDto,TimePeriodDto>,ITimePeriodAppService
    {

        public  TimePeriodAppService(IRepository<TimePeriod,long> repository) : base(repository) { }

        [AbpAllowAnonymous]
        public override Task<PagedResultDto<TimePeriodDto>> GetAll(PagedResultRequestDto input)
        {
            if (input.MaxResultCount == 0)
            {
                input.MaxResultCount = int.MaxValue;
            }
            return base.GetAll(input);
        }
    }
}
