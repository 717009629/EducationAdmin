using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using EducationAdmin.Authorization;
using EducationAdmin.Lessons.Dto;
using EducationAdmin.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EducationAdmin.Lessons
{
    [AbpAuthorize(PermissionNames.Pages_Lesson)]
    public class LessonAppService : AsyncCrudAppService<Lesson, LessonsDto, long, PagedLessonResultRequestDto, CreateLessionDto, LessonsDto>, ILessonAppService
    {
        public LessonAppService(IRepository<Lesson, long> repository) : base(repository)
        {
            this.CreatePermissionName = PermissionNames.Pages_Lesson + ".Create";
            this.UpdatePermissionName = PermissionNames.Pages_Lesson + ".Edit";
            this.DeletePermissionName = PermissionNames.Pages_Lesson + ".Delete";
        }

        protected override IQueryable<Lesson> CreateFilteredQuery(PagedLessonResultRequestDto input)
        {

            return Repository.GetAllIncluding(m => m.Student, m => m.Teacher)
                  .WhereIf(input.StudentId != null, m => m.StudentId == input.StudentId)
                  .WhereIf(input.TeacherId != null, m => m.TeacherId == input.TeacherId)
                  .WhereIf(input.StartDate != null, m => m.LessionDate >= input.StartDate)
                  .WhereIf(input.EndDate != null, m => m.LessionDate <= input.EndDate);
        }
    }
}
