using Abp.Application.Services;
using Abp.Application.Services.Dto;
using EducationAdmin.CourseItems.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.CourseItems
{
    public interface ICourseItemAppService:IAsyncCrudAppService<CourseItemDto, long, PagedResultRequestDto, CreateCourseItemDto, CourseItemDto>
    {
    }
}
