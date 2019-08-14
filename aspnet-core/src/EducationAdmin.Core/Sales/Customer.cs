using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.Sales
{
    public class Customer : FullAuditedEntity<long>, IMustHaveTenant
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
        public int TenantId { get; set; }
    }

    public enum CustomerState
    {
        Created=0,

        FollowUp=1,

        Droped=2,

        Success=3
    }
}
