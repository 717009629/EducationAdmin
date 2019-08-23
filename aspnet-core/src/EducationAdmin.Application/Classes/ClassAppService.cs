using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.UI;
using Abp.UI.Inputs;
using EducationAdmin.Authorization;
using EducationAdmin.Classes.Dto;
using EducationAdmin.Education;
using EducationAdmin.Sales;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationAdmin.Classes
{
    [AbpAuthorize(PermissionNames.Pages_Classes)]
    public class ClassAppService : AsyncCrudAppService<Class, ClassDto, long, PagedClassResultRequestDto, CreateClassDto, EditClassDto>, IClassAppService
    {
        private readonly IRepository<Order, long> OrderRepository;
        public ClassAppService(IRepository<Class, long> repository, IRepository<Order, long> orderRepository) : base(repository)
        {

            DeletePermissionName = PermissionNames.Pages_Classes + ".Delete";
            CreatePermissionName = PermissionNames.Pages_Classes + ".Create";
            UpdatePermissionName = PermissionNames.Pages_Classes + ".Edit";
            OrderRepository = orderRepository;
        }

        public async Task AddOrders(AddOrderDto input)
        {
            var clas = await Repository.FirstOrDefaultAsync(m => m.Id == input.ClassId);
            if (clas.State == ClassState.Closed)
                throw new Exception();

            var order = await OrderRepository.FirstOrDefaultAsync(m => input.OrderId == m.Id);
            if (order.State == OrderState.Created)
                throw new Exception();

            if (order.ClassId != null)
                throw new Exception();

            if (order.CourseId != clas.CourseId)
                throw new Exception();


            order.ClassId = clas.Id;

            await OrderRepository.UpdateAsync(order);

        }

        protected override IQueryable<Class> CreateFilteredQuery(PagedClassResultRequestDto input)
        {

            return Repository.GetAllIncluding(m => m.Course, m => m.Teacher);
        }
    }
}
