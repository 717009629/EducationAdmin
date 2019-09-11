using Abp.Application.Services.Dto;
using EducationAdmin.Sales;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.PrivateClass.Dto
{
    public class PagedPrivateClassResultRequestDto:PagedResultRequestDto
    {

        public long? StudentId { get; set; }


        public long? ClassId { get; set; }


    }
}
