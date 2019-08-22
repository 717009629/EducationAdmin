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
using Microsoft.Extensions.Configuration;
using Abp.AutoMapper;

namespace EducationAdmin.Contracts
{
    [AbpAuthorize(PermissionNames.Pages_Contracts)]
    public class ContractAppService : AsyncCrudAppService<Contract, ContractDto, long, PagedContractResultRequestDto, CreateContractDto, EditContractDto>, IContractAppService
    {
        private readonly IRepository<Order, long> OrderRepository;
        public ContractAppService(IRepository<Contract, long> repository, IRepository<Order, long> orderRepository) : base(repository)
        {
            this.OrderRepository = orderRepository;
            DeletePermissionName = PermissionNames.Pages_Contracts + ".Delete";
            CreatePermissionName = PermissionNames.Pages_Contracts + ".Create";
            UpdatePermissionName = PermissionNames.Pages_Contracts + ".Edit";
        }

        public override async Task<ContractDto> Create(CreateContractDto input)
        {
            var order = await OrderRepository.GetAllIncluding(m => m.Course).SingleAsync(m => m.Id == input.Id);
            input.StudentId = order.StudentId;
            input.State = ContractState.Created;
            input.SalesmanId = this.AbpSession.UserId.Value;
            return await base.Create(input);
        }

        public async Task<ContractDto> Audite(AuditeDto input)
        {
            if(input.ContractState== ContractState.Created||input.ContractState== ContractState.WaitAudite)
            {
                throw new AbpException();
            }
            CheckPermission(PermissionNames.Pages_Contracts + ".Audite");
            var contract = await Repository.FirstOrDefaultAsync(m => m.Id == input.ContractId);
            contract.State = input.ContractState;
            contract.AuditedReason = input.Reason;
            var r =await Repository.UpdateAsync(contract);
            return ObjectMapper.Map<ContractDto>(r);
        }


        public override Task<ContractDto> Update(EditContractDto input)
        {
            input.State = ContractState.WaitAudite;
            return base.Update(input);
        }

        protected override IQueryable<Contract> CreateFilteredQuery(PagedContractResultRequestDto input)
        {
            return Repository.GetAllIncluding(m => m.Salesman, m => m.Student, m => m.Order, m => m.Order.Course)
                    .WhereIf(input.StudentId != null, m => m.StudentId == input.StudentId)
                    .WhereIf(!input.StudentName.IsNullOrWhiteSpace(), x => x.Student.Name.Contains(input.StudentName));
        }
    }
}
