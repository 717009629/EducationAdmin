using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using EducationAdmin.Roles.Dto;
using EducationAdmin.Users.Dto;

namespace EducationAdmin.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedUserResultRequestDto, CreateUserDto, UserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();

        Task ChangeLanguage(ChangeUserLanguageDto input);
    }
}
