using Abp.AutoMapper;
using EducationAdmin.Sales;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.Contracts.Dto
{
    [AutoMapTo()]
    public class AuditeDto
    {
        public long ContractId { get; set; }
        public ContractState ContractState { get; set; }
        public string Reason { get; set; }
    }
}
