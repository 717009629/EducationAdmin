using Abp.Application.Services;
using EducationAdmin.Lessons.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EducationAdmin.Lessons
{
    public interface ILessonAppService:IAsyncCrudAppService<LessonDto,long,PagedLessonResultRequestDto, CreateLessionDto, EditLessionDto>
    {
        Task<List<StudentLessonDto>> GetByStudentId(int studentId, DateTime start, DateTime end);
    }
}
