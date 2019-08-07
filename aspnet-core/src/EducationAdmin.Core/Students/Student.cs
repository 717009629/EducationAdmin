using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using EducationAdmin.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EducationAdmin.Students
{
    public class Student :FullAuditedEntity<long>,IMayHaveTenant
    {
        public string Name { get; set; }

        public string Province { get; set; }

        public string City { get; set; }

        public string Address { get; set; }


        public bool? Sex { get; set; }


        public string Parent { get; set; }

        public string Relation { get; set; }

        public string Phone { get; set; }

        public string Mother { get; set; }

        public string Father { get; set; }

        public string FatherPhone { get; set; }

        public string MotherPhone { get; set; }

        public string School { get; set; }

        
        public DateTime? Birthday { get; set; }


        public string Grade { get; set; }

        public string CourseType { get; set; }

        public string StudentType { get; set; }

        public string TeachMethod { get; set; }

        public string Origin { get; set; }

        public string Note { get; set; }
        public long SalesmanId { get; set; }

        [ForeignKey(nameof(SalesmanId))]
        public User Salesman { get; set; }
        public int? TenantId { get ; set ; }

       // public string SalesmanName { get { return Salesman?.Name; } }
    }
}
