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
    [AbpAuthorize(PermissionNames.Pages_Orders)]
    public class OrderAppService : AsyncCrudAppService<Order, OrderDto, long, PagedOrderResultRequestDto, CreateOrderDto, EditOrderDto>, IOrderAppService
    {
        public OrderAppService(IRepository<Order, long> repository) : base(repository)
        {
            DeletePermissionName = PermissionNames.Pages_Orders + ".Delete";
            CreatePermissionName = PermissionNames.Pages_Orders + ".Create";
            UpdatePermissionName = PermissionNames.Pages_Orders + ".Edit";
        }


        protected override IQueryable<Order> CreateFilteredQuery(PagedOrderResultRequestDto input)
        {
            return Repository.GetAllIncluding(m => m.Salesman, m => m.Student, m => m.Course, m => m.Class)
                    .WhereIf(input.StudentId != null, m => m.StudentId == input.StudentId)
                    .WhereIf(input.ClassId != null, m => m.ClassId == input.ClassId)
                    .WhereIf(input.CourseId != null, m => m.CourseId == input.CourseId&&m.ClassId==null)
                     .WhereIf(input.State != null, m => m.State == input.State)
                    .WhereIf(!input.StudentName.IsNullOrWhiteSpace(), x => x.Student.Name.Contains(input.StudentName));
        }

        public override Task<OrderDto> Create(CreateOrderDto input)
        {
            input.SalesmanId = this.AbpSession.UserId.Value;
            return base.Create(input);
        }

        public override async Task<OrderDto> Update(EditOrderDto input)
        {
            var order = await Repository.FirstOrDefaultAsync(m => m.Id == input.Id);
            if (order.State == OrderState.Audited)
                throw new Exception();
            return await base.Update(input);
        }

        public async Task<OrderDto> Audite(AuditeOrderDto input)
        {
            CheckPermission(PermissionNames.Pages_Contracts + ".Audite");
            var order = await Repository.FirstOrDefaultAsync(m => m.Id == input.OrderId);
            if (order.State == OrderState.Audited)
                throw new Exception();

            order.State = OrderState.Audited;
            var r = await Repository.UpdateAsync(order);
            return ObjectMapper.Map<OrderDto>(r);
        }
    }
}
