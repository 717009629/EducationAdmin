using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.UI;
using EducationAdmin.Authorization;
using EducationAdmin.Education;
using EducationAdmin.Orders.Dto;
using EducationAdmin.PrivateClass.Dto;
using EducationAdmin.Sales;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationAdmin.PrivateClasses
{
    [AbpAuthorize(PermissionNames.Pages_PrivateClasses)]
    public class PrivateClassAppService : ApplicationService, IPrivateClassAppService
    {
        IRepository<Order, long> Repository;
        public PrivateClassAppService(IRepository<Order, long> repository)
        {
            this.Repository = repository;
        }




        public async Task<PagedResultDto<PrivateClassDto>> GetAll(PagedPrivateClassResultRequestDto input)
        {
            if (input.MaxResultCount == 0)
            {
                input.MaxResultCount = int.MaxValue;
            }
            var query = Repository.GetAllIncluding(m=>m.LessonAttendances,m=>m.Class,m=>m.Student,m=>m.Class.Teacher,m=>m.Course)
                .Where(m => m.Course.ClassType == ClassType.OneToOne && m.State != OrderState.Created)
                .WhereIf(input.StudentId != null, m => m.StudentId == input.StudentId)
                .WhereIf(input.ClassId != null, m => m.ClassId == input.ClassId);
            var count = await query.CountAsync();
            var list = await query.Skip(input.SkipCount).Take(input.MaxResultCount).ToListAsync();
            return new PagedResultDto<PrivateClassDto>(count, ObjectMapper.Map<List<PrivateClassDto>>(list));
        }
    }
}
