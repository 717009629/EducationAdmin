using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using EducationAdmin.Authorization;
using EducationAdmin.Customers.Dto;
using EducationAdmin.Sales;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EducationAdmin.Customers
{
    [AbpAuthorize(PermissionNames.Pages_Customers)]
    public class CustomerAppService : AsyncCrudAppService<Customer, CustomerDto, long, PagedCustomerResultRequestDto, CreateCustomerDto, CustomerDto>, ICustomerAppService
    {
        public CustomerAppService(IRepository<Customer, long> repository) : base(repository)
        {
            DeletePermissionName = PermissionNames.Pages_Customers + ".Delete";
            CreatePermissionName = PermissionNames.Pages_Customers + ".Create";
            UpdatePermissionName = PermissionNames.Pages_Customers + ".Edit";
        }

        protected override IQueryable<Customer> CreateFilteredQuery(PagedCustomerResultRequestDto input)
        {
            return base.CreateFilteredQuery(input)
                .WhereIf(!string.IsNullOrEmpty(input.Keyword), m => m.StudentName.Contains(input.Keyword) || m.FatherPhone.Contains(input.Keyword) || m.MotherPhone.Contains(input.Keyword) || m.OtherGuadianPhone.Contains(input.Keyword))
                .WhereIf(!string.IsNullOrEmpty(input.State), m => m.State == input.State);
        }
    }
}
