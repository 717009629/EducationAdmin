using Abp.AutoMapper;
using EducationAdmin.Sales;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.Customers.Dto
{
    [AutoMapTo(typeof(Customer))]
    public  class CreateCustomerDto
    {
        public string StudentName { get; set; }


        public string Mother { get; set; }

        public string Father { get; set; }

        public string FatherPhone { get; set; }

        public string MotherPhone { get; set; }

        public string OtherGuardian { get; set; }


        public string OtherGuadianPhone { get; set; }

        public string Note { get; set; }

        public CustomerState State { get; set; }
    }
}
