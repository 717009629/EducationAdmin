using Abp.Domain.Entities.Auditing;
using EducationAdmin.Authorization.Users;
using EducationAdmin.Sales;
using EducationAdmin.Students;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.Education
{
    public class Class:FullAuditedEntity<long>
    {
        public string Name { get; set; }


        public List<Order>  Orders { get; set; }

        public List<Lesson> Lessons { get; set; }   

        public long CourseId { get; set; }

        public Course Course { get; set; }

        public User Teacher { get; set; }

        public long TeacherId { get; set; }

        public ClassState State { get; set; }

        
    }


    public enum ClassType
    {
        OneToMany=0,
        OnoToOne=1
    }

    public enum ClassState
    {
        Created=0,

        Close=1
    }
}
