using Abp.Application.Services;
using Abp.Application.Services.Dto;
using EducationAdmin.Contracts.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.Contracts
{
    public interface IContractAppService : IAsyncCrudAppService<ContractDto, long, PagedContractResultRequestDto, CreateContractDto, EditContractDto>
    {
    }
}
