using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.Customers.Dto
{
    public class PagedCustomerResultRequestDto:PagedResultRequestDto
    {
        public string StudentName { get; set; }
        public string SalesmanName { get; set; }
    }
}
