using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using EducationAdmin.Authorization.Users;
using EducationAdmin.Teachers.Dto;
using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Abp.Linq.Extensions;
using AutoMapper;
using EducationAdmin.Authorization;
using Abp.Authorization;
using EducationAdmin.Settings;
using EducationAdmin.Education;

namespace EducationAdmin.Teachers
{
    [AbpAuthorize(PermissionNames.Pages_Teachers)]
    public class TeacherAppService : ApplicationService, ITeacherAppService
    {
        private readonly IRepository<User, long> Repository;

        private readonly IRepository<TimePeriod, long> TimePeriodRepository;
        private readonly IRepository<Lesson, long> LessionRepository;
        public TeacherAppService(IRepository<User, long> repository, IRepository<TimePeriod, long> timePeriodRepository, IRepository<Lesson, long> lessionRepository)
        {
            Repository = repository;
            TimePeriodRepository = timePeriodRepository;
            LessionRepository = lessionRepository;
        }

        

        public async Task<PagedResultDto<TeacherDto>> GetAll(PagedTeacherResultRequestDto input)
        {
            if (input.MaxResultCount == 0)
            {
                input.MaxResultCount = int.MaxValue;
            }
            var query = Repository.GetAll().Where(m => m.Type == UserType.Teacher).WhereIf(input.Name != null, m => m.Name.Contains(input.Name));
            var count = await query.CountAsync();
            var list = await query.Skip(input.SkipCount).Take(input.MaxResultCount).ToListAsync();
            return new PagedResultDto<TeacherDto>(count, ObjectMapper.Map<List<TeacherDto>>(list));
        }

        public async Task<List<TeacherDto>> GetAllAvailable(DateTime date, long? timePeriodId,long? lessonId)
        {
            if (timePeriodId == null)
            {
                return new List<TeacherDto>();
            }

            date = date.Date;
            var period = await TimePeriodRepository.FirstOrDefaultAsync(timePeriodId.Value);
            var tids = await LessionRepository.GetAll().WhereIf(lessonId!=null,m=>m.Id!=lessonId).Where(m => m.LessonDate == date && m.Start < period.End && m.End > period.Start).Select(m => m.TeacherId).ToListAsync();

            var list=  await Repository.GetAll().Where(m => m.Type == UserType.Teacher&&!tids.Contains(m.Id)).ToListAsync();
            return ObjectMapper.Map<List<TeacherDto>>(list);
        }
    }
}
