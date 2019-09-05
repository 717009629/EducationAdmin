using Abp.Application.Services;
using Abp.Authorization;
using Abp.AutoMapper;
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
        private readonly IRepository<LessonAttendance, long> LessonAttendanceRepository;
        public LessonAppService(IRepository<Lesson, long> repository, IRepository<TimePeriod, long> timePeriodRepository, IRepository<LessonAttendance, long> lessonAttendanceRepository) : base(repository)
        {
            this.CreatePermissionName = PermissionNames.Pages_Lessons + ".Create";
            this.UpdatePermissionName = PermissionNames.Pages_Lessons + ".Edit";
            this.DeletePermissionName = PermissionNames.Pages_Lessons + ".Delete";
            TimePeriodRepository = timePeriodRepository;
            LessonAttendanceRepository = lessonAttendanceRepository;
        }


        public async Task<List<StudentLessonDto>> GetByStudentId(int studentId, DateTime start, DateTime end)
        {
            var lessons = await Repository.GetAllIncluding(m => m.Class, m => m.Teacher).Where(m => m.Class.Orders.Any(n => n.StudentId == studentId) && m.LessonDate >= start && m.LessonDate <= end).ToListAsync();
            var attends = await LessonAttendanceRepository.GetAll().Where(m => m.Order.StudentId == studentId && m.Lesson.LessonDate >= start && m.Lesson.LessonDate <= end).ToListAsync();
            var sls = ObjectMapper.Map<List<StudentLessonDto>>(lessons);
            foreach (var s in sls)
            {
                var at = attends.FirstOrDefault(m => m.LessonId == s.Id);

                s.Attended = at?.Attended;
            }
            return sls;
        }
        protected override IQueryable<Lesson> CreateFilteredQuery(PagedLessonResultRequestDto input)
        {

            return base.CreateFilteredQuery(input).Include(m => m.Class).Include(m => m.Teacher)
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
            var count = await Repository.CountAsync(m => m.ClassId == input.ClassId && m.LessonDate.Date == input.LessonDate && m.End > input.Start && m.Start < input.End);
            if (count > 0)
            {
                throw new UserFriendlyException("The Class has  been assigned a course in the same time,please select another lesson index. ");
            }
            count = await Repository.CountAsync(m => m.TeacherId == input.TeacherId && m.LessonDate.Date == input.LessonDate && m.End > input.Start && m.Start < input.End);
            if (count > 0)
            {
                throw new UserFriendlyException("The Teacher has  been assigned a course in the same time,please select another lesson index. ");
            }
            var period = await TimePeriodRepository.FirstOrDefaultAsync(m => m.Id == input.TimePeriodId);
            input.Start = period.Start;
            input.End = period.End;
            return await base.Create(input);
        }

        public override async Task<LessonDto> Update(EditLessionDto input)
        {
            var lesson = await Repository.FirstOrDefaultAsync(m => m.Id == input.Id);
            if (lesson.IsFinish)
            {
                throw new Exception();

            }
            input.LessonDate = input.LessonDate.Date;
            var count = await Repository.CountAsync(m => m.ClassId == input.ClassId && m.LessonDate.Date == input.LessonDate && m.End > input.Start && input.Start < m.End && m.Id != input.Id);
            if (count > 0)
            {
                throw new UserFriendlyException("The Class has  been assigned a course in the same time,please select another lesson index. ");
            }
            count = await Repository.CountAsync(m => m.TeacherId == input.TeacherId && m.LessonDate.Date == input.LessonDate && m.End > input.Start && input.Start < m.End && m.Id != input.Id);
            if (count > 0)
            {
                throw new UserFriendlyException("The Teacher has  been assigned a course in the same time,please select another lesson index. ");
            }
            var period = await TimePeriodRepository.FirstOrDefaultAsync(m => m.Id == input.TimePeriodId);
            input.Start = period.Start;
            input.End = period.End;
            return await base.Update(input);
        }
        protected override IQueryable<Lesson> ApplySorting(IQueryable<Lesson> query, PagedLessonResultRequestDto input)
        {
            return base.ApplySorting(query, input).OrderBy(m => m.LessonDate).ThenBy(m => m.Start);
        }
    }
}
