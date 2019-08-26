using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using AutoMapper;
using EducationAdmin.Authorization;
using EducationAdmin.Education;
using EducationAdmin.Settings;
using EducationAdmin.TimePeriods.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace EducationAdmin.TimePeriods
{
    [AbpAuthorize(PermissionNames.Pages_TimePeriods)]
    public class TimePeriodAppService : AsyncCrudAppService<TimePeriod, TimePeriodDto, long, PagedTimePeriodResultRequestDto, CreateTimePeriodDto, EditTimePeriodDto>, ITimePeriodAppService
    {
        private readonly IRepository<Lesson, long> LessonRepository;
        public TimePeriodAppService(IRepository<TimePeriod, long> repository, IRepository<Lesson, long> lessonRepository) : base(repository)
        {
            this.CreatePermissionName = PermissionNames.Pages_TimePeriods + ".Create";
            this.UpdatePermissionName = PermissionNames.Pages_TimePeriods + ".Edit";
            this.DeletePermissionName = PermissionNames.Pages_TimePeriods + ".Delete";
            LessonRepository = lessonRepository;
        }

        [AbpAuthorize]
        public override Task<PagedResultDto<TimePeriodDto>> GetAll(PagedTimePeriodResultRequestDto input)
        {

            return base.GetAll(input);
        }

        protected override IQueryable<TimePeriod> CreateFilteredQuery(PagedTimePeriodResultRequestDto input)
        {
            if (input.MaxResultCount == 0)
            {
                input.MaxResultCount = int.MaxValue;
            }
            // return Repository.GetAll().OrderBy(m=>m.Start)
            return base.CreateFilteredQuery(input)
                .WhereIf(input.IsActive != null, m => m.IsActive == input.IsActive);

        }
        protected override IQueryable<TimePeriod> ApplyPaging(IQueryable<TimePeriod> query, PagedTimePeriodResultRequestDto input)
        {
            if (input.MaxResultCount == 0)
            {
                input.MaxResultCount = int.MaxValue;
            }
            return base.ApplyPaging(query, input);
        }

        protected override IQueryable<TimePeriod> ApplySorting(IQueryable<TimePeriod> query, PagedTimePeriodResultRequestDto input)
        {
            return base.ApplySorting(query, input).OrderBy(m => m.Start);
        }


        public override Task<TimePeriodDto> Create(CreateTimePeriodDto input)
        {
            if (input.Start >= input.End)
            {
                throw new UserFriendlyException("The end time can't less than the start time!");
            }
           
            return base.Create(input);
        }

        public async Task<List<TimePeriodDto>> GetAllAvailable(DateTime date, long classId, long? lessonId)
        {
            date = date.Date;
            var p1 = await Repository.GetAll().Where(m => m.IsActive).OrderBy(m=>m.Start).ThenBy(m=>m.End).ToListAsync();
            var p2 = await LessonRepository.GetAllIncluding(m => m.TimePeriod)
                .WhereIf(lessonId != null, m => m.Id != lessonId)
                .Where(m => m.LessonDate == date && m.ClassId == classId)
                .Select(m => m.TimePeriod).ToListAsync();

            IEnumerable<TimePeriod> list = p1;
            foreach (var p in p2)
            {
                list = list.Where(m => m.End <= p.Start||m.Start>=p.End);
            }
            
            return ObjectMapper.Map<List<TimePeriodDto>>(list);

        }
    }
}
