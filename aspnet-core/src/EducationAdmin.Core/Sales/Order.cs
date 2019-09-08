﻿using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using EducationAdmin.Authorization.Users;
using EducationAdmin.Education;
using EducationAdmin.Students;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.Sales
{
    public class Order : FullAuditedEntity<long>, IMustHaveTenant
    {


        public int TenantId { get; set; }

        public DateTime? OrderDate { get; set; }

        public DateTime? SchoolBegin { get; set; }

        public decimal FullMoney { get; set; }

        public OrderState State { get; set; }

        public string ClassName { get; set; }

        public long StudentId { get; set; }

        public Student Student { get; set; }

        public long CourseId { get; set; }
        public Course Course { get; set; }

        public long SalesmanId { get; set; }

        public User Salesman { get; set; }

        public string Note { get; set; }

        public Contract Contract { get; set; }

        public long? ClassId { get; set; }

        public Class Class { get; set; }

        public int Count { get; set; }

        public DateTime? AuditeTime { get; set; }

        public DateTime? FinishTime { get; set; }

        public List<LessonAttendance> LessonAttendances { get; set; }
    }

    public enum OrderState
    {
        Created=0,
        Audited=1,
        LessonFinished=2
    }
}
