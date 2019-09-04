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
using EducationAdmin.Sales;

namespace EducationAdmin.LessonAttendances
{
    [AbpAuthorize(PermissionNames.Pages_LessonAttendances)]
    public class LessonAttendanceAppService : AsyncCrudAppService<LessonAttendance, LessonAttendanceDto, long, PagedLessonAttendanceResultRequestDto, CreateLessonAttendanceDto, EditLessonAttendanceDto>, ILessonAttendanceAppService
    {
        private readonly IRepository<Lesson, long> LessonRepository;
        private readonly IRepository<Order, long> OrderRepository;
        public LessonAttendanceAppService(IInsertMutiRepository<LessonAttendance, long> repository, IRepository<Lesson, long> lessonRepository, IRepository<Order, long> orderRepository) : base(repository)
        {
            this.CreatePermissionName = PermissionNames.Pages_LessonAttendances + ".Create";
            this.UpdatePermissionName = PermissionNames.Pages_LessonAttendances + ".Edit";
            this.DeletePermissionName = PermissionNames.Pages_LessonAttendances + ".Delete";
            LessonRepository = lessonRepository;
            OrderRepository = orderRepository;
        }

        [AbpAuthorize]
        public override Task<PagedResultDto<LessonAttendanceDto>> GetAll(PagedLessonAttendanceResultRequestDto input)
        {

            return base.GetAll(input);
        }

        protected override IQueryable<LessonAttendance> CreateFilteredQuery(PagedLessonAttendanceResultRequestDto input)
        {
            // return Repository.GetAll().OrderBy(m=>m.Start)
            return base.CreateFilteredQuery(input).Include(m=>m.Order.Student).Include(m=>m.Lesson.Class).Include(m=>m.Lesson.Teacher).WhereIf(input.LessonId!=null, m=>m.LessonId==input.LessonId);

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

        public async Task CreateMult(CreateMultLessonAttendanceDto input)
        {
            var lesson = await LessonRepository.FirstOrDefaultAsync(input.LessonId);
            if (lesson.IsFinish)
            {
                throw new Exception();
            }
            lesson.IsFinish = true;
            var orders = await OrderRepository.GetAll().Where(m => m.ClassId == lesson.ClassId).ToListAsync();
            var list = orders.Select(m => new LessonAttendance
            {
                OrderId = m.Id,
                LessonId = lesson.Id,
                Attended = input.OrderIds.Contains(m.Id),
                CreationTime = DateTime.Now,
                CreatorUserId = AbpSession.UserId,
                TenantId = AbpSession.TenantId.Value
            });
            await ((IInsertMutiRepository<LessonAttendance, long>)Repository).InsertListAsync(list);
        }
    }
}
