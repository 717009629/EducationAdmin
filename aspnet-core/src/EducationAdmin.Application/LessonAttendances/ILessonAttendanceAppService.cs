using Abp.Application.Services;
using Abp.Application.Services.Dto;
using EducationAdmin.LessonAttendances.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EducationAdmin.LessonAttendances
{
    public interface ILessonAttendanceAppService:IAsyncCrudAppService<LessonAttendanceDto,long, PagedLessonAttendanceResultRequestDto, CreateLessonAttendanceDto, EditLessonAttendanceDto>
    {
        Task CreateMult(CreateMultLessonAttendanceDto input);
    }
}
