using Abp.Application.Services;
using EducationAdmin.Sales.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.Sales
{
    public interface IStudentAppService : IAsyncCrudAppService<StudentDto, long, PagedStudentResultRequestDto, CreateStudentDto,StudentDto>
    {
    }
}
