using Abp.Application.Services;
using Abp.Application.Services.Dto;
using EducationAdmin.MultiTenancy.Dto;

namespace EducationAdmin.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

