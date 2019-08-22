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

namespace EducationAdmin.Teachers
{
    [AbpAuthorize(PermissionNames.Pages_Teachers)]
    public class TeacherAppService : ApplicationService, ITeacherAppService
    {
        private readonly IRepository<User, long> Repository;
        public TeacherAppService(IRepository<User, long> repository)
        {
            Repository = repository;
        }

        public async Task<PagedResultDto<TeacherDto>> GetAll(PagedTeacherResultRequestDto input)
        {
            var query = Repository.GetAll().Where(m => m.Type == UserType.Teacher).WhereIf(input.Name != null, m => m.Name.Contains(input.Name));
            var count = await query.CountAsync();
            var list = await query.Skip(input.SkipCount).Take(input.MaxResultCount).ToListAsync();
            return new PagedResultDto<TeacherDto>(count, ObjectMapper.Map<List<TeacherDto>>(list));
        }
    }
}
