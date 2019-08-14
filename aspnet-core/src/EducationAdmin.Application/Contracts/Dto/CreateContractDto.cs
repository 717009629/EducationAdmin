using Abp.AutoMapper;
using EducationAdmin.Sales;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.Contracts.Dto
{
    [AutoMapTo(typeof(Contract))]
    public class CreateContractDto
    {
        public long Id { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal FullMoney { get; set; }

        public ContractState State { get; set; }

        public string Note { get; set; }

        public string AuditedReson { get; set; }

        public long StudentId { get; set; }


        public long SalesmanId { get; set; }

    }
}
