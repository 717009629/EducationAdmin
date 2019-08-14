using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using EducationAdmin.Authorization;
using EducationAdmin.Customers.Dto;
using EducationAdmin.Sales;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.Customers
{
    [AbpAuthorize(PermissionNames.Pages_Customer)]
    public class CustomerAppService:AsyncCrudAppService<Customer,CustomerDto,long,PagedCustomerResultRequestDto,CreateCustomerDto,CustomerDto>,ICustomerAppService
    {
        public CustomerAppService(IRepository<Customer,long> repository) : base(repository)
        {
            DeletePermissionName = PermissionNames.Pages_Customer + ".Delete";
            CreatePermissionName = PermissionNames.Pages_Customer + ".Create";
            UpdatePermissionName = PermissionNames.Pages_Customer + ".Edit";
        }
    }
}
