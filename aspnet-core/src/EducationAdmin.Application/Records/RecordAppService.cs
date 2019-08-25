using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using EducationAdmin.Authorization;
using EducationAdmin.Records.Dto;
using EducationAdmin.Sales;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Linq.Extensions;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using EducationAdmin.Orders.Dto;
using Abp.Extensions;

namespace EducationAdmin.Records
{
    [AbpAuthorize(PermissionNames.Pages_Records)]
    public class RecordAppService : AsyncCrudAppService<Record, RecordDto, long, PagedRecordResultRequestDto, CreateRecordDto, RecordDto>, IRecordAppService
    {
        private readonly IRepository<Customer, long> CustomerRepository;
        public RecordAppService(IRepository<Record, long> repository, IRepository<Customer, long> customerRepository) : base(repository)
        {
            CustomerRepository = customerRepository;

            DeletePermissionName = PermissionNames.Pages_Records + ".Delete";
            CreatePermissionName = PermissionNames.Pages_Records + ".Create";
            UpdatePermissionName = PermissionNames.Pages_Records + ".Edit";
        }


        protected override IQueryable<Record> CreateFilteredQuery(PagedRecordResultRequestDto input)
        {
            return base.CreateFilteredQuery(input).Include(m => m.Salesman).Include(m => m.Customer)
                    .WhereIf(input.CustomerId != null, m => m.CustomerId == input.CustomerId)
                    .WhereIf(!input.StudentName.IsNullOrWhiteSpace(), x => x.Customer.StudentName.Contains(input.StudentName));
        }

        public override async Task<RecordDto> Create(CreateRecordDto input)
        {
            input.SalesmanId = this.AbpSession.UserId.Value;
            var customer = await CustomerRepository.FirstOrDefaultAsync(input.CustomerId);
            customer.State = input.State;

            return await base.Create(input);
        }

        public override Task<RecordDto> Update(RecordDto input)
        {
            return base.Update(input);
        }
    }
}
