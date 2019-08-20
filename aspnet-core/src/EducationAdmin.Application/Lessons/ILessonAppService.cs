using Abp.Application.Services;
using EducationAdmin.Lessons.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.Lessons
{
    public interface ILessonAppService:IAsyncCrudAppService<LessonsDto,long,PagedLessonResultRequestDto, CreateLessionDto, EditLessionDto>
    {
    }
}
