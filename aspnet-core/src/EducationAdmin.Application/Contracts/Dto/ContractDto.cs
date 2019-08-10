﻿using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using EducationAdmin.Students;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.Contracts.Dto
{
    [AutoMapFrom(typeof(Contract))]
    [AutoMapTo(typeof(Contract))]
    public class ContractDto:FullAuditedEntityDto<long>
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal FullMoney { get; set; }

        public ContractState State { get; set; }

        public string Note { get; set; }

        public string AuditedReson { get; set; }

        public long StudentId { get; set; }
        public string StudentName { get; set; }

        public long AuditorId { get; set; }
        public string AuditorName { get; set; }

        public long SalesmanId { get; set; }

        public string SalesmanName { get; set; }
    }
}
