﻿using Abp.Application.Services.Dto;
using EducationAdmin.Sales;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.Orders.Dto
{
    public class PagedOrderResultRequestDto:PagedResultRequestDto
    {

        public long? StudentId { get; set; }
        public string StudentName { get; set; }
        public string SalesmanName { get; set; }

        public long? ClassId { get; set; }

        public long? CourseId { get; set; }

        public OrderState? State { get; set; }
    }
}
