using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Linq.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using EducationAdmin.Authorization;
using EducationAdmin.Sales.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EducationAdmin.Students;
using Microsoft.EntityFrameworkCore;
using Abp.UI;

namespace EducationAdmin.Sales
{

    [AbpAuthorize(PermissionNames.Pages_Students)]
    public class StudentAppService : AsyncCrudAppService<Student, StudentDto, long, PagedStudentResultRequestDto, CreateStudentDto, EditStudentDto>, IStudentAppService
    {
       public StudentAppService(IRepository<Student,long > repository) : base(repository) {
            DeletePermissionName = PermissionNames.Pages_Students + ".Delete";
            CreatePermissionName = PermissionNames.Pages_Students + ".Create";
            UpdatePermissionName = PermissionNames.Pages_Students + ".Edit";
            LocalizationSourceName = EducationAdminConsts.LocalizationSourceName;
        }

        public override async  Task<StudentDto> Create(CreateStudentDto input)
        {
            var student = await Repository.FirstOrDefaultAsync(m => m.Phone == input.Phone);
            if (student != null)
            {
                throw new UserFriendlyException(L("StudentHasSamePhone"));
            }
            return await base.Create(input);
        }

        public override async Task<StudentDto> Update(EditStudentDto input)
        {
            var student = await Repository.FirstOrDefaultAsync(m => m.Phone == input.Phone&&m.Id!=input.Id);
            if (student != null)
            {
                throw new UserFriendlyException(L("StudentHasSamePhone"));
            }
            return await base.Update(input);
        }



        protected override IQueryable<Student> CreateFilteredQuery(PagedStudentResultRequestDto input)
        {
            return base.CreateFilteredQuery(input).Include(x => x.Teacher)
               .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Keyword) );
        }

    }
}
