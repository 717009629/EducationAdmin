using Abp.Application.Services;
using Abp.Application.Services.Dto;
using EducationAdmin.Courses.Dto;
using EducationAdmin.Education;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.Courses
{
    public interface ICourseAppService:IAsyncCrudAppService<CourseDto,long,PagedResultRequestDto,CreateCourseDto,CourseDto>
    {
    }
}
