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
using EducationAdmin.Orders.Dto;

namespace EducationAdmin.Records
{
    [AbpAuthorize(PermissionNames.Pages_Record)]
    public class RecordAppService : AsyncCrudAppService<Record, RecordDto, long, PagedRecordResultRequestDto, CreateRecordDto, RecordDto>, IRecordAppService
    {
        public RecordAppService(IRepository<Record, long> repository) : base(repository) { }


        protected override IQueryable<Record> CreateFilteredQuery(PagedRecordResultRequestDto input)
        {
          return  Repository.GetAllIncluding(m => m.Salesman).WhereIf(input.StudentId!=null, m => m.StudentId == input.StudentId);
        }

        public override Task<RecordDto> Create(CreateRecordDto input)
        {
            input.SalesmanId = this.AbpSession.UserId.Value;
            return base.Create(input);
        }
    }
}
