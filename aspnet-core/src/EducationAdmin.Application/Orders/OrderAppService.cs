using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using EducationAdmin.Orders.Dto;
using EducationAdmin.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EducationAdmin.Orders
{
    public class OrderAppService : AsyncCrudAppService<Order, OrderDto, long, PagedOrderResultRequestDto, CreateOrderDto, OrderDto>, IOrderAppService
    {
        public OrderAppService (IRepository<Order,long> repository) : base(repository) { }


        protected override IQueryable<Order> CreateFilteredQuery(PagedOrderResultRequestDto input)
        {
            return Repository.GetAllIncluding(m => m.Student, m => m.Salesman).WhereIf(input.StudentId != null, m => m.StudentId == input.StudentId);
        }
    }
}
