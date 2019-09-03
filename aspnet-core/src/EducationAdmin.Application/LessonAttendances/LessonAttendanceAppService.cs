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
using EducationAdmin.LessonAttendances.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using EducationAdmin.Repository;

namespace EducationAdmin.LessonAttendances
{
    [AbpAuthorize(PermissionNames.Pages_LessonAttendances)]
    public class LessonAttendanceAppService : AsyncCrudAppService<LessonAttendance, LessonAttendanceDto, long, PagedLessonAttendanceResultRequestDto, CreateLessonAttendanceDto, EditLessonAttendanceDto>, ILessonAttendanceAppService
    {
        private readonly IRepository<Lesson, long> LessonRepository;
        public LessonAttendanceAppService(IInsertMutiRepository<LessonAttendance, long> repository, IRepository<Lesson, long> lessonRepository) : base(repository)
        {
            this.CreatePermissionName = PermissionNames.Pages_LessonAttendances + ".Create";
            this.UpdatePermissionName = PermissionNames.Pages_LessonAttendances + ".Edit";
            this.DeletePermissionName = PermissionNames.Pages_LessonAttendances + ".Delete";
            LessonRepository = lessonRepository;
        }

        [AbpAuthorize]
        public override Task<PagedResultDto<LessonAttendanceDto>> GetAll(PagedLessonAttendanceResultRequestDto input)
        {

            return base.GetAll(input);
        }

        protected override IQueryable<LessonAttendance> CreateFilteredQuery(PagedLessonAttendanceResultRequestDto input)
        {
            if (input.MaxResultCount == 0)
            {
                input.MaxResultCount = int.MaxValue;
            }
            // return Repository.GetAll().OrderBy(m=>m.Start)
            return base.CreateFilteredQuery(input);

        }
        protected override IQueryable<LessonAttendance> ApplyPaging(IQueryable<LessonAttendance> query, PagedLessonAttendanceResultRequestDto input)
        {
            if (input.MaxResultCount == 0)
            {
                input.MaxResultCount = int.MaxValue;
            }
            return base.ApplyPaging(query, input);
        }


        public override async Task<LessonAttendanceDto> Create(CreateLessonAttendanceDto input)
        {

            return await base.Create(input);
        }

        public override async Task<LessonAttendanceDto> Update(EditLessonAttendanceDto input)
        {

            return await base.Update(input);
        }

    }
}
