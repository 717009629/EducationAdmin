using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.UI;
using EducationAdmin.Authorization;
using EducationAdmin.Education;
using EducationAdmin.Orders.Dto;
using EducationAdmin.Sales;
using Microsoft.EntityFrameworkCore;
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
            return base.CreateFilteredQuery(input).Include(m => m.Salesman).Include(m => m.Student).Include(m => m.Course).Include(m => m.Class).Include(m => m.LessonAttendances)
                    .WhereIf(input.StudentId != null, m => m.StudentId == input.StudentId)
                    .WhereIf(input.ClassId != null, m => m.ClassId == input.ClassId)
                    .WhereIf(input.CourseId != null, m => m.CourseId == input.CourseId && m.ClassId == null)
                     .WhereIf(input.State != null, m => m.State == input.State)
                    .WhereIf(!input.StudentName.IsNullOrWhiteSpace(), x => x.Student.Name.Contains(input.StudentName));
        }

        public override async Task<OrderDto> Create(CreateOrderDto input)
        {
            input.SalesmanId = this.AbpSession.UserId.Value;
            var order = await Repository.FirstOrDefaultAsync(m => m.CourseId == input.CourseId && m.StudentId == input.StudentId && m.State != OrderState.LessonFinished);
            if (order != null)
                throw new UserFriendlyException("There is a order with the same course and it's not finished!");
            return await base.Create(input);
        }

        public override async Task<OrderDto> Update(EditOrderDto input)
        {
            var orders = await Repository.GetAll().Where(m => m.Id == input.Id || (m.CourseId == input.CourseId && m.StudentId == input.StudentId && m.State != OrderState.LessonFinished)).ToListAsync();
            if (orders.Count >= 2)
            {
                throw new UserFriendlyException("There is a order with the same course and it's not finished!");
            }
            var order = orders.FirstOrDefault(m => m.Id == input.Id);
            if (order.State != OrderState.Created)
                throw new Exception();
            return await base.Update(input);
        }

        public async Task<OrderDto> Audite(AuditeOrderDto input)
        {
            CheckPermission(PermissionNames.Pages_Orders + ".Audite");
            var order = await Repository.GetAllIncluding(m => m.Course, m => m.Student).FirstOrDefaultAsync(m => m.Id == input.OrderId);
            if (order.State == OrderState.Audited)
                throw new Exception();

            order.State = OrderState.Audited;
            if (order.Course.ClassType == ClassType.OneToOne)
            {
                order.Class = new Class
                {
                    CourseId = order.CourseId,
                    CreationTime = DateTime.Now,
                    CreatorUserId = this.AbpSession.UserId,
                    Name = $"{order.Course.Name}--{order.StudentId}--{order.Student.Name}",
                    State = ClassState.Created,
                    TeacherId = order.Student.TeacherId
                };
            }
            var r = await Repository.UpdateAsync(order);
            return ObjectMapper.Map<OrderDto>(r);
        }
    }
}
