using Abp.AutoMapper;
using EducationAdmin.Education;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.Classes.Dto
{
    [AutoMapTo(typeof(Class))]
    public class CreateClassDto
    {
        public string Name { get; set; }



        public long CourseId { get; set; }

        public Course Course { get; set; }

        public long TeacherId { get; set; }
        public string Note { get; set; }
    }
}
