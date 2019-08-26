using Abp.Application.Services;
using Abp.Application.Services.Dto;
using EducationAdmin.TimePeriods.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EducationAdmin.TimePeriods
{
    public interface ITimePeriodAppService:IAsyncCrudAppService<TimePeriodDto,long, PagedTimePeriodResultRequestDto, CreateTimePeriodDto, EditTimePeriodDto>
    {

        Task<List<TimePeriodDto>> GetAllAvailable(DateTime date, long classId, long? lessonId);
    }
}
