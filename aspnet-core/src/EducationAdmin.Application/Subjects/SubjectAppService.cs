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
using EducationAdmin.Subjects.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace EducationAdmin.Subjects
{
    [AbpAuthorize(PermissionNames.Pages_Subjects)]
    public class SubjectAppService : AsyncCrudAppService<Subject, SubjectDto, long, PagedSubjectResultRequestDto, CreateSubjectDto, EditSubjectDto>, ISubjectAppService
    {
        private readonly IRepository<Lesson, long> LessonRepository;
        public SubjectAppService(IRepository<Subject, long> repository, IRepository<Lesson, long> lessonRepository) : base(repository)
        {
            this.CreatePermissionName = PermissionNames.Pages_Subjects + ".Create";
            this.UpdatePermissionName = PermissionNames.Pages_Subjects + ".Edit";
            this.DeletePermissionName = PermissionNames.Pages_Subjects + ".Delete";
            LessonRepository = lessonRepository;
        }

        [AbpAuthorize]
        public override Task<PagedResultDto<SubjectDto>> GetAll(PagedSubjectResultRequestDto input)
        {

            return base.GetAll(input);
        }

        protected override IQueryable<Subject> CreateFilteredQuery(PagedSubjectResultRequestDto input)
        {
            if (input.MaxResultCount == 0)
            {
                input.MaxResultCount = int.MaxValue;
            }
            // return Repository.GetAll().OrderBy(m=>m.Start)
            return base.CreateFilteredQuery(input)
                .WhereIf(input.IsActive != null, m => m.IsActive == input.IsActive);

        }
        protected override IQueryable<Subject> ApplyPaging(IQueryable<Subject> query, PagedSubjectResultRequestDto input)
        {
            if (input.MaxResultCount == 0)
            {
                input.MaxResultCount = int.MaxValue;
            }
            return base.ApplyPaging(query, input);
        }

        public override async Task<SubjectDto> Create(CreateSubjectDto input)
        {
            input.Name = input.Name.Trim();
            var d = await Repository.FirstOrDefaultAsync(m => m.Name == input.Name );
            if (d != null)
            {
                throw new UserFriendlyException("The subject name can't be repeated!");
            }
            return await base.Create(input);
        }

        public override async Task<SubjectDto> Update(EditSubjectDto input)
        {
            input.Name = input.Name.Trim();
            var d = await Repository.FirstOrDefaultAsync(m => m.Name == input.Name && m.Id != input.Id);
            if (d != null)
            {
                throw new UserFriendlyException("The subject name can't be repeated!");
            }
            return await base.Update(input);
        }

    }
}
