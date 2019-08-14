using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using EducationAdmin.CourseItems.Dto;
using EducationAdmin.Education;
using EducationAdmin.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationAdmin.CourseItems
{
    public class CourseItemAppService : AsyncCrudAppService<CourseItem, CourseItemDto, long, PagedCourseResultRequestDto, CreateCourseItemDto, CourseItemDto>, ICourseItemAppService
    {
        private readonly IRepository<Course, long> CourseRepository;
        public CourseItemAppService(IRepository<CourseItem, long> repository, IRepository<Course, long> courseRepository) : base(repository)
        {
            CourseRepository = courseRepository;
        }

        protected override IQueryable<CourseItem> CreateFilteredQuery(PagedCourseResultRequestDto input)
        {
            return Repository.GetAllIncluding(m => m.Course).WhereIf(input.OrderId!=null,m=>m.OrderId==input.OrderId);
        }

        public override async Task<CourseItemDto> Create(CreateCourseItemDto input)
        {
            var course = await CourseRepository.FirstOrDefaultAsync(input.CourseId);
            input.FullMoney = input.Count * course.Price;
            return await base.Create(input);
        }

        public override async Task<CourseItemDto> Update(CourseItemDto input)
        {
            var course = await CourseRepository.FirstOrDefaultAsync(input.CourseId);
            input.FullMoney = input.Count * course.Price;
            return await base.Update(input);
        }
    }
}
