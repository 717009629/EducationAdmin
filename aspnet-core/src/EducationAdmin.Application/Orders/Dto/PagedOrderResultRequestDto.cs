using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.Orders.Dto
{
    public class PagedOrderResultRequestDto:PagedResultRequestDto
    {

        public int? StudentId { get; set; }
        public string StudentName { get; set; }
        public string SalesmanName { get; set; }
    }
}
