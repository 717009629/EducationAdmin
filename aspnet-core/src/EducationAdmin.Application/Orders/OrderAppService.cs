using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using EducationAdmin.Authorization;
using EducationAdmin.Orders.Dto;
using EducationAdmin.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EducationAdmin.Orders
{
    [AbpAuthorize(PermissionNames.Pages_Order)]
    public class OrderAppService : AsyncCrudAppService<Order, OrderDto, long, PagedOrderResultRequestDto, CreateOrderDto, OrderDto>, IOrderAppService
    {
        public OrderAppService (IRepository<Order,long> repository) : base(repository) { }


        protected override IQueryable<Order> CreateFilteredQuery(PagedOrderResultRequestDto input)
        {
            return Repository.GetAllIncluding(m => m.Student, m => m.Salesman).WhereIf(input.StudentId != null, m => m.StudentId == input.StudentId);
        }
    }
}
