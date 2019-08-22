using Abp.AutoMapper;
using EducationAdmin.Education;
using EducationAdmin.Students;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.Lessons.Dto
{
    [AutoMapTo(typeof(Lesson))]
    public class CreateLessionDto
    {

        public DateTime LessonDate { get; set; }


        public long ClassId { get; set; }

        public int LessonIndex { get; set; }

        public bool IsFinish { get; set; }

        public long TeacherId { get; set; }


        public string Course { get; set; }
    }
}
