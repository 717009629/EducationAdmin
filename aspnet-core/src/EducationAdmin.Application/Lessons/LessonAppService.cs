using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using EducationAdmin.Authorization;
using EducationAdmin.Lessons.Dto;
using EducationAdmin.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationAdmin.Lessons
{
    [AbpAuthorize(PermissionNames.Pages_Lesson)]
    public class LessonAppService : AsyncCrudAppService<Lesson, LessonsDto, long, PagedLessonResultRequestDto, CreateLessionDto, EditLessionDto>, ILessonAppService
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
                  .WhereIf(input.Start != null, m => m.LessonDate >= input.Start)
                  .WhereIf(input.End != null, m => m.LessonDate < input.End);
        }


        public override async Task<LessonsDto> Create(CreateLessionDto input)
        {
            input.LessonDate = input.LessonDate.Date;
            var count =await Repository.CountAsync(m => m.StudentId == input.StudentId && m.LessonIndex == input.LessonIndex && m.LessonDate.Date == input.LessonDate);
            if (count > 0)
            {
                throw new UserFriendlyException("The Student has  been assigned a course in the same time,please select another lesson index. ");
            }
            return await base.Create(input);
        }

        public override async Task<LessonsDto> Update(EditLessionDto input)
        {
            input.LessonDate = input.LessonDate.Date;
            var count= await Repository.CountAsync(m => m.StudentId == input.StudentId && m.LessonIndex == input.LessonIndex && m.LessonDate.Date == input.LessonDate&&m.Id==input.Id);
            if (count > 0)
            {
                throw new UserFriendlyException("The Student has  been assigned a course in the same time,please select another lesson index. ");
            }

            return await base.Update(input);
        }

    }
}
