using Abp.AutoMapper;
using EducationAdmin.Students;
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

        public string State { get; set; }

        public string ClassName { get; set; }

        public long StudentId { get; set; }

    }
}
