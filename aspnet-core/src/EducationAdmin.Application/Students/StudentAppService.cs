using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Linq.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using EducationAdmin.Authorization;
using EducationAdmin.Students.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationAdmin.Students
{

    [AbpAuthorize(PermissionNames.Pages_Students)]
    public class StudentAppService : AsyncCrudAppService<Student, StudentDto, Guid, PagedStudentResultRequestDto, CreateStudentDto, StudentDto>, IStudentAppService
    {
       public StudentAppService(IRepository<Student,Guid > repository) : base(repository) { }

        public override Task<StudentDto> Create(CreateStudentDto input)
        {
            input.SalesmanId = this.AbpSession.UserId.Value;
            return base.Create(input);
        }

        public override Task<StudentDto> Update(StudentDto input)
        {
            return base.Update(input);
        }

        protected override IQueryable<Student> CreateFilteredQuery(PagedStudentResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.Salesman)
               .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Keyword) );
        }
    }
}
