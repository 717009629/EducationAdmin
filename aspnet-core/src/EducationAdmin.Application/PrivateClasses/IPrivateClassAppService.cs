using Abp.Application.Services;
using Abp.Application.Services.Dto;
using EducationAdmin.Orders.Dto;
using EducationAdmin.PrivateClass.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EducationAdmin.PrivateClasses
{
    public interface IPrivateClassAppService : IApplicationService
    {
        Task<PagedResultDto<PrivateClassDto>> GetAll(PagedPrivateClassResultRequestDto input);
    }
}
