using Abp.Application.Services;
using Abp.Application.Services.Dto;
using EducationAdmin.Subjects.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EducationAdmin.Subjects
{
    public interface ISubjectAppService:IAsyncCrudAppService<SubjectDto,long, PagedSubjectResultRequestDto, CreateSubjectDto, EditSubjectDto>
    {
    }
}
