using Abp.Application.Services;
using Abp.Application.Services.Dto;
using EducationAdmin.Records.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EducationAdmin.Records
{
    public interface IRecordAppService : IAsyncCrudAppService<RecordDto, long, PagedResultRequestDto, CreateRecordDto, RecordDto>
    {

        Task<ListResultDto<RecordDto>> GetRecordsByStudentId(long id);
    }
}
