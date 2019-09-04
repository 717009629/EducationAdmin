using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using EducationAdmin.Classes.Dto;
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
    [AutoMapFrom(typeof(Order))]
    [AutoMapTo(typeof(Order))]
    public class OrderDto : FullAuditedEntityDto<long>
    {

        public DateTime? OrderDate { get; set; }

        public DateTime? SchoolBegin { get; set; }

        public decimal FullMoney { get; set; }

        public OrderState State { get; set; }

        public long StudentId { get; set; }

        public StudentDto Student { get; set; }

        public long CourseId { get; set; }
        public CourseDto Course { get; set; }

        //public OrderContractDto Contract { get; set; }

        public long SalesmanId { get; set; }

        public string SalesmanName { get; set; }

        public string Note { get; set; }

        public long? ClassId { get; set; }

        public ClassDto Class { get; set; }
    }
}
