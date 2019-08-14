using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using EducationAdmin.Contracts.Dto;
using EducationAdmin.Sales;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Linq;
using Microsoft.EntityFrameworkCore;
using Abp;
using Abp.Domain.Entities;
using System.Linq;
using Abp.Linq.Extensions;
using Abp.Extensions;
using Abp.Authorization;
using EducationAdmin.Authorization;
using EducationAdmin.MultiTenancy;

namespace EducationAdmin.Contracts
{
    [AbpAuthorize(PermissionNames.Pages_Contract)]
    public class ContractAppService : AsyncCrudAppService<Contract, ContractDto, long, PagedContractResultRequestDto, CreateContractDto, EditContractDto>, IContractAppService
    {
        private readonly IRepository<Order, long> OrderRepository;
        public ContractAppService(IRepository<Contract, long> repository, IRepository<Order, long> orderRepository) : base(repository)
        {
            this.OrderRepository = orderRepository;
            DeletePermissionName = PermissionNames.Pages_Contract + ".Delete";
            CreatePermissionName = PermissionNames.Pages_Contract + ".Create";
            UpdatePermissionName = PermissionNames.Pages_Contract + ".Edit";
        }

        public override async Task<ContractDto> Create(CreateContractDto input)
        {
            var order = await OrderRepository.GetAllIncluding(m => m.Course).SingleAsync(m => m.Id == input.Id);
            input.StudentId = order.StudentId;
            input.State = ContractState.Created;
            input.SalesmanId = this.AbpSession.UserId.Value;
            return await base.Create(input);
        }

        protected override IQueryable<Contract> CreateFilteredQuery(PagedContractResultRequestDto input)
        {
            return Repository.GetAllIncluding(m => m.Salesman, m => m.Student)
                    .WhereIf(input.StudentId != null, m => m.StudentId == input.StudentId)
                    .WhereIf(!input.StudentName.IsNullOrWhiteSpace(), x => x.Student.Name.Contains(input.StudentName));
        }
    }
}
