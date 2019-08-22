using Abp.Application.Services;
using EducationAdmin.Classes.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.Classes
{
    public interface IClassAppService:IAsyncCrudAppService<ClassDto, long,PagedClassResultRequestDto, CreateClassDto,EditClassDto>
    {
    }
}
