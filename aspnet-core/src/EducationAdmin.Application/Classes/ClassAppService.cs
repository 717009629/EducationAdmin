using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Linq.Extensions;
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
        private readonly IRepository<Course, long> CourseRepository;
        public ClassAppService(IRepository<Class, long> repository, IRepository<Order, long> orderRepository, IRepository<Course, long> courseRepository) : base(repository)
        {

            DeletePermissionName = PermissionNames.Pages_Classes + ".Delete";
            CreatePermissionName = PermissionNames.Pages_Classes + ".Create";
            UpdatePermissionName = PermissionNames.Pages_Classes + ".Edit";
            OrderRepository = orderRepository;
            CourseRepository = courseRepository;
        }

        public override async Task<ClassDto> Create(CreateClassDto input)
        {
            var course = await CourseRepository.FirstOrDefaultAsync(m => m.Id == input.CourseId);
            if (course.ClassType == ClassType.OneToOne)
                throw new Exception();
            return await base.Create(input);
        }

        public async Task AddOrders(AddOrderDto input)
        {
            var clas = await Repository.GetAllIncluding(m => m.Course).FirstOrDefaultAsync(m => m.Id == input.ClassId);
            if (clas.State == ClassState.Closed)
                throw new Exception();
            if (clas.Course.ClassType == ClassType.OneToOne)
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

            return base.CreateFilteredQuery(input).Include(m => m.Course).Include(m => m.Teacher)
                .WhereIf(input.ClassType != null, m => m.Course.ClassType == input.ClassType);
        }
    }
}
