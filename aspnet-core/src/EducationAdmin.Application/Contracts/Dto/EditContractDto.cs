using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using EducationAdmin.Students;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.Contracts.Dto
{
    [AutoMapTo(typeof(Contract))]
    public class EditContractDto:EntityDto<long>
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal FullMoney { get; set; }

        public ContractState State { get; set; }

        public string Note { get; set; }
    }
}
