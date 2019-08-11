using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.Contracts.Dto
{
   public class PagedContractResultRequestDto: PagedResultRequestDto
    {
        public long? StudentId { get; set; }
        public string StudentName { get; set; }
        public string SalesmanName { get; set; }
    }
}
