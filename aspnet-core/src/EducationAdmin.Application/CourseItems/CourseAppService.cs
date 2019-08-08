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
    public class CourseItemAppService: AsyncCrudAppService<CourseItem, CourseItemDto, long, PagedResultRequestDto, CreateCourseItemDto, CourseItemDto>,ICourseItemAppService
    {
        public CourseItemAppService(IRepository<CourseItem, long> repository) : base(repository) { }
    }
}
