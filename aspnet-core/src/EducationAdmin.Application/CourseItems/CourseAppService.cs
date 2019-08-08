using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using EducationAdmin.CourseItems.Dto;
using EducationAdmin.Education;
using EducationAdmin.Students;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.CourseItems
{
    public class CourseAppService: AsyncCrudAppService<CourseItem, CourseItemDto, long, PagedResultRequestDto, CreateCourseItemDto, CourseItemDto>,ICourseAppService
    {
        public CourseAppService(IRepository<CourseItem, long> repository) : base(repository) { }
    }
}
