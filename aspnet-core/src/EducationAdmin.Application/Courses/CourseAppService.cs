﻿using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using EducationAdmin.Authorization;
using EducationAdmin.Courses.Dto;
using EducationAdmin.Education;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Abp.Linq.Extensions;

namespace EducationAdmin.Courses
{
    [AbpAuthorize(PermissionNames.Pages_Courses)]
    public class CourseAppService : AsyncCrudAppService<Course, CourseDto, long, PagedCourseResultRequestDto, CreateCourseDto, EditCourseDto>, ICourseAppService
    {

        public CourseAppService(IRepository<Course, long> repository) : base(repository)
        {
            DeletePermissionName = PermissionNames.Pages_Courses + ".Delete";
            CreatePermissionName = PermissionNames.Pages_Courses + ".Create";
            UpdatePermissionName = PermissionNames.Pages_Courses + ".Edit";
        }

        protected override IQueryable<Course> CreateFilteredQuery(PagedCourseResultRequestDto input)
        {
           

            return base.CreateFilteredQuery(input)
                .WhereIf(input.ClassType != null, m => m.ClassType == input.ClassType);
        }

        protected override IQueryable<Course> ApplyPaging(IQueryable<Course> query, PagedCourseResultRequestDto input)
        {
            if (input.MaxResultCount == 0)
            {
                input.MaxResultCount = int.MaxValue;
            }
            return base.ApplyPaging(query, input);
        }

    }

}
