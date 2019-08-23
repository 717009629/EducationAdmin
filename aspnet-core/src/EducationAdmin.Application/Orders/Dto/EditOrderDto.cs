using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using EducationAdmin.Contracts.Dto;
using EducationAdmin.Courses.Dto;
using EducationAdmin.Sales;
using EducationAdmin.Sales.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.Orders.Dto
{
    [AutoMapTo(typeof(Order))]
    public class EditOrderDto : FullAuditedEntityDto<long>
    {

        public DateTime? OrderDate { get; set; }

        public DateTime? SchoolBegin { get; set; }

        public decimal FullMoney { get; set; }

   


        public long StudentId { get; set; }


        public long CourseId { get; set; }

        public long SalesmanId { get; set; }


        public string Note { get; set; }
    }
}
