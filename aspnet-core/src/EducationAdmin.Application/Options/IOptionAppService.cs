using Abp.Application.Services;
using Abp.Application.Services.Dto;
using EducationAdmin.Options.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EducationAdmin.Options
{
    public interface IOptionAppService:IAsyncCrudAppService<OptionDto,long, PagedOptionResultRequestDto, CreateOptionDto, EditOptionDto>
    {
    }
}
