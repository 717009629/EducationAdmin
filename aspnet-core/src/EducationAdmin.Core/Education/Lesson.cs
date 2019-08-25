﻿using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using EducationAdmin.Authorization.Users;
using EducationAdmin.Sales;
using EducationAdmin.Settings;
using EducationAdmin.Students;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.Education
{
    public class Lesson : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        public DateTime LessonDate { get; set; }


        public long ClassId { get; set; }

        public Class Class { get; set; }

       // public int LessonNumber { get; set; }

        public bool IsFinish { get; set; }

        public long TeacherId { get; set; }

        public User Teacher { get; set; }

        public string Course { get; set; }

        public long TimePeriodId { get; set; }

        public TimePeriod TimePeriod { get; set; }

        public TimeSpan Start { get; set; }

        public TimeSpan End { get; set; }
    }
}
