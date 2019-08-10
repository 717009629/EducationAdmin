using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using EducationAdmin.Contracts.Dto;
using EducationAdmin.Students;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Linq;
using Microsoft.EntityFrameworkCore;

namespace EducationAdmin.Contracts
{
    public class ContractAppService : AsyncCrudAppService<Contract, ContractDto, long, PagedResultRequestDto, CreateContractDto, ContractDto>, IContractAppService
    {
        private readonly IRepository<Order, long> OrderRepository;
        public ContractAppService(IRepository<Contract, long> repository, IRepository<Order, long> orderRepository) : base(repository)
        {
            this.OrderRepository = orderRepository;
        }

        public override async Task<ContractDto> Create(CreateContractDto input)
        {
            var order = await OrderRepository.GetAllIncluding(m => m.CourseItems).SingleAsync(m => m.Id == input.Id);
            if (order.CourseItems.Count == 0)
            {

            }
            input.StudentId = order.StudentId;
            input.State = ContractState.Created;
            return await base.Create(input);
        }

    }
}
