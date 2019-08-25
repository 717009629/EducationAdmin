using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using EducationAdmin.Authorization;
using EducationAdmin.Education;
using EducationAdmin.Lessons.Dto;
using EducationAdmin.Settings;
using EducationAdmin.Students;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationAdmin.Lessons
{
    [AbpAuthorize(PermissionNames.Pages_Lessons)]
    public class LessonAppService : AsyncCrudAppService<Lesson, LessonDto, long, PagedLessonResultRequestDto, CreateLessionDto, EditLessionDto>, ILessonAppService
    {

        private readonly IRepository<TimePeriod, long> TimePeriodRepository;
        public LessonAppService(IRepository<Lesson, long> repository, IRepository<TimePeriod, long> timePeriodRepository) : base(repository)
        {
            this.CreatePermissionName = PermissionNames.Pages_Lessons + ".Create";
            this.UpdatePermissionName = PermissionNames.Pages_Lessons + ".Edit";
            this.DeletePermissionName = PermissionNames.Pages_Lessons + ".Delete";
            TimePeriodRepository = timePeriodRepository;
        }

        protected override IQueryable<Lesson> CreateFilteredQuery(PagedLessonResultRequestDto input)
        {
            
            return base.CreateFilteredQuery(input).Include(m => m.Class).Include( m => m.Teacher)
                  .WhereIf(input.StudentId != null, m => m.Class.Orders.Any(n => n.StudentId == input.StudentId))
                  .WhereIf(input.TeacherId != null, m => m.TeacherId == input.TeacherId)
                  .WhereIf(input.ClassId != null, m => m.ClassId == input.ClassId)
                  .WhereIf(input.Start != null, m => m.LessonDate >= input.Start)
                  .WhereIf(input.End != null, m => m.LessonDate < input.End);
        }

        protected override IQueryable<Lesson> ApplyPaging(IQueryable<Lesson> query, PagedLessonResultRequestDto input)
        {
            if (input.MaxResultCount == 0)
            {
                input.MaxResultCount = int.MaxValue;
            }
            return base.ApplyPaging(query, input);
        }
        public override async Task<LessonDto> Create(CreateLessionDto input)
        {
            input.LessonDate = input.LessonDate.Date;
            //var count =await Repository.CountAsync(m => m.StudentId == input.StudentId && m.LessonIndex == input.LessonIndex && m.LessonDate.Date == input.LessonDate);
            //if (count > 0)
            //{
            //    throw new UserFriendlyException("The Student has  been assigned a course in the same time,please select another lesson index. ");
            //}
            var period = await TimePeriodRepository.FirstOrDefaultAsync(m => m.Id == input.TimePeriodId);
            input.Start = period.Start;
            input.End = period.End;
            return await base.Create(input);
        }

        public override async Task<LessonDto> Update(EditLessionDto input)
        {
            input.LessonDate = input.LessonDate.Date;
            //var count= await Repository.CountAsync(m => m.StudentId == input.StudentId && m.LessonIndex == input.LessonIndex && m.LessonDate.Date == input.LessonDate&&m.Id==input.Id);
            //if (count > 0)
            //{
            //    throw new UserFriendlyException("The Student has  been assigned a course in the same time,please select another lesson index. ");
            //}
            var period = await TimePeriodRepository.FirstOrDefaultAsync(m => m.Id == input.TimePeriodId);
            input.Start = period.Start;
            input.End = period.End;
            return await base.Update(input);
        }

    }
}
