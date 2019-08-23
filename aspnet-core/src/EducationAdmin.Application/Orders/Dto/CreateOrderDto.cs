using Abp.AutoMapper;
using EducationAdmin.Sales;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.Orders.Dto
{
    [AutoMapTo(typeof(Order))]
   public class CreateOrderDto
    {
        public DateTime? OrderDate { get; set; }

        public DateTime? SchoolBegin { get; set; }

        public decimal FullMoney { get; set; }

        public long StudentId { get; set; }

        public long CourseId { get; set; }

        public string Note { get; set; }

        public long SalesmanId { get; set; }

    }
}
