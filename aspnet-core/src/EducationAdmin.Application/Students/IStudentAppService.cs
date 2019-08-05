using Abp.Application.Services;
using EducationAdmin.Students.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.Students
{
    public interface IStudentAppService : IAsyncCrudAppService<StudentDto, Guid, StudentDto, CreateStudentDto,StudentDto>
    {
    }
}
