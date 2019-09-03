using Abp.Domain.Entities.Auditing;
using EducationAdmin.Students;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.Education
{
    public class LessonAttendance:FullAuditedEntity<long>
    {
        public  long StudentId { get; set; }

        public Student Student { get; set; }

        public long LessonId { get; set; }

        public Lesson Lesson { get; set; }

        public bool Attended { get; set; }
    }
}
