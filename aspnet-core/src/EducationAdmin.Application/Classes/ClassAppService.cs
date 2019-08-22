using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using EducationAdmin.Authorization;
using EducationAdmin.Classes.Dto;
using EducationAdmin.Education;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EducationAdmin.Classes
{
    [AbpAuthorize(PermissionNames.Pages_Classes)]
    public class ClassAppService : AsyncCrudAppService<Class, ClassDto, long, PagedClassResultRequestDto, CreateClassDto, EditClassDto>, IClassAppService
    {
        public ClassAppService(IRepository<Class, long> repository) : base(repository)
        {

            DeletePermissionName = PermissionNames.Pages_Classes + ".Delete";
            CreatePermissionName = PermissionNames.Pages_Classes + ".Create";
            UpdatePermissionName = PermissionNames.Pages_Classes + ".Edit";
        }


        protected override IQueryable<Class> CreateFilteredQuery(PagedClassResultRequestDto input)
        {

            return Repository.GetAllIncluding(m => m.Course, m => m.Teacher);
        }
    }
}
