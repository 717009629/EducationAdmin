using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using EducationAdmin.Authorization;
using EducationAdmin.Orders.Dto;
using EducationAdmin.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationAdmin.Orders
{
    [AbpAuthorize(PermissionNames.Pages_Order)]
    public class OrderAppService : AsyncCrudAppService<Order, OrderDto, long, PagedOrderResultRequestDto, CreateOrderDto, EditOrderDto>, IOrderAppService
    {
        public OrderAppService(IRepository<Order, long> repository) : base(repository)
        {
            DeletePermissionName = PermissionNames.Pages_Order + ".Delete";
            CreatePermissionName = PermissionNames.Pages_Order + ".Create";
            UpdatePermissionName = PermissionNames.Pages_Order + ".Edit";
        }


        protected override IQueryable<Order> CreateFilteredQuery(PagedOrderResultRequestDto input)
        {
            return Repository.GetAllIncluding(m => m.Salesman, m => m.Student,m=>m.Course, m=>m.Contract)
        .WhereIf(input.StudentId != null, m => m.StudentId == input.StudentId)
        .WhereIf(!input.StudentName.IsNullOrWhiteSpace(), x => x.Student.Name.Contains(input.StudentName));
        }

        public override Task<OrderDto> Create(CreateOrderDto input)
        {
            input.SalesmanId = this.AbpSession.UserId.Value;
            return base.Create(input);
        }

        public override Task<OrderDto> Update(EditOrderDto input)
        {
            return base.Update(input);
        }
    }
}
