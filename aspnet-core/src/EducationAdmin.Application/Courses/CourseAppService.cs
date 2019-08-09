using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using EducationAdmin.Authorization;
using EducationAdmin.Courses.Dto;
using EducationAdmin.Education;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EducationAdmin.Courses
{
    [AbpAuthorize(PermissionNames.Pages_Course)]
    public class CourseAppService:AsyncCrudAppService<Course,CourseDto,long,PagedResultRequestDto,CreateCourseDto,CourseDto>,ICourseAppService
    {
        public CourseAppService(IRepository<Course,long> repository) : base(repository) { }
    }
}
