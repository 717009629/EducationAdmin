﻿using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using EducationAdmin.Contracts.Dto;
using EducationAdmin.Students;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.Contracts
{
    public class ContractAppSerive:AsyncCrudAppService<Contract,ContractDto,long,PagedResultRequestDto,CreateContractDto,ContractDto>,IContractAppService
    {

        public ContractAppSerive( IRepository<Contract,long> repository) : base(repository) { }
    }
}
