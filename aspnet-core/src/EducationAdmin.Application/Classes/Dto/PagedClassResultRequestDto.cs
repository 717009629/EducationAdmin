﻿using Abp.Application.Services.Dto;
using EducationAdmin.Education;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.Classes.Dto
{
    public class PagedClassResultRequestDto:PagedResultRequestDto
    {
        public ClassType? ClassType { get; set; }
    }
}
