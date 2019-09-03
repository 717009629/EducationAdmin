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
using EducationAdmin.Options.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace EducationAdmin.Options
{
    [AbpAuthorize(PermissionNames.Pages_Options)]
    public class OptionAppService : AsyncCrudAppService<Option, OptionDto, long, PagedOptionResultRequestDto, CreateOptionDto, EditOptionDto>, IOptionAppService
    {
        private readonly IRepository<Lesson, long> LessonRepository;
        public OptionAppService(IRepository<Option, long> repository, IRepository<Lesson, long> lessonRepository) : base(repository)
        {
            this.CreatePermissionName = PermissionNames.Pages_Options + ".Create";
            this.UpdatePermissionName = PermissionNames.Pages_Options + ".Edit";
            this.DeletePermissionName = PermissionNames.Pages_Options + ".Delete";
            LessonRepository = lessonRepository;
        }

        [AbpAuthorize]
        public override Task<PagedResultDto<OptionDto>> GetAll(PagedOptionResultRequestDto input)
        {

            return base.GetAll(input);
        }

        protected override IQueryable<Option> CreateFilteredQuery(PagedOptionResultRequestDto input)
        {
            if (input.MaxResultCount == 0)
            {
                input.MaxResultCount = int.MaxValue;
            }
            // return Repository.GetAll().OrderBy(m=>m.Start)
            return base.CreateFilteredQuery(input)
                .WhereIf(input.IsActive != null, m => m.IsActive == input.IsActive);

        }
        protected override IQueryable<Option> ApplyPaging(IQueryable<Option> query, PagedOptionResultRequestDto input)
        {
            if (input.MaxResultCount == 0)
            {
                input.MaxResultCount = int.MaxValue;
            }
            return base.ApplyPaging(query, input);
        }


        public override async Task<OptionDto> Create(CreateOptionDto input)
        {
            input.Name = input.Name.Trim();
            var d = await Repository.FirstOrDefaultAsync(m => m.Name == input.Name && m.Category == input.Category);
            if (d != null)
            {
                throw new UserFriendlyException("The name can't be repeated");
            }
            return await base.Create(input);
        }

        public override async Task<OptionDto> Update(EditOptionDto input)
        {
            input.Name = input.Name.Trim();
            var d = await Repository.FirstOrDefaultAsync(m => m.Name == input.Name && m.Category == input.Category && m.Id != input.Id);
            if (d != null)
            {
                throw new UserFriendlyException("The name can't be repeated!");
            }
            return await base.Update(input);
        }

    }
}
