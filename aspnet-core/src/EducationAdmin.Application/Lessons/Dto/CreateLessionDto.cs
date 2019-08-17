using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.Lessons.Dto
{
    public class CreateLessionDto
    {
        public DateTime LessionDate { get; set; }


        public long StudentId { get; set; }


        public long OrderId { get; set; }

        public bool IsFinish { get; set; }

        public long TeacherId { get; set; }


        public string Course { get; set; }
    }
}
