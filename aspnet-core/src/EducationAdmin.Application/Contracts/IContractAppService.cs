using Abp.Application.Services;
using Abp.Application.Services.Dto;
using EducationAdmin.Contracts.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EducationAdmin.Contracts
{
    public interface IContractAppService : IAsyncCrudAppService<ContractDto, long, PagedContractResultRequestDto, CreateContractDto, EditContractDto>
    {
        Task<ContractDto> Audite(AuditeDto input);
    }
}
