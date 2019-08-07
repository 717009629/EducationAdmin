using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using EducationAdmin.Authorization;
using EducationAdmin.Records.Dto;
using EducationAdmin.Students;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Linq.Extensions;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EducationAdmin.Records
{
    [AbpAuthorize(PermissionNames.Pages_Students)]
    public class RecordAppService : AsyncCrudAppService<Record, RecordDto, long, PagedResultRequestDto, CreateRecordDto, RecordDto>, IRecordAppService
    {
        public RecordAppService(IRepository<Record, long> repository) : base(repository) { }

        public Task<ListResultDto<RecordDto>> GetRecordsByStudentId(long id)
        {
            var list = this.Repository.GetAllIncluding(m=>m.Salesman).Where(m => m.StudentId == id);
            return Task.FromResult(new ListResultDto<RecordDto>(ObjectMapper.Map<List<RecordDto>>(list)));
        }

        public override Task<RecordDto> Create(CreateRecordDto input)
        {
            input.SalesmanId = this.AbpSession.UserId.Value;
            return base.Create(input);
        }
    }
}
