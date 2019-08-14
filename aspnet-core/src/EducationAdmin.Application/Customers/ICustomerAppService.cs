using Abp.Application.Services;
using EducationAdmin.Customers.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.Customers
{
    public  interface ICustomerAppService:IAsyncCrudAppService<CustomerDto,long,PagedCustomerResultRequestDto,CreateCustomerDto,CustomerDto>
    {
    }
}
