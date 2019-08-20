using Abp.Application.Services;
using Abp.Application.Services.Dto;
using EducationAdmin.Teachers.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EducationAdmin.Teachers
{
    public interface ITeacherAppService:IApplicationService
    {
       Task<PagedResultDto<TeacherDto>> GetAll(PagedTeacherResultRequestDto input);
    }
}
