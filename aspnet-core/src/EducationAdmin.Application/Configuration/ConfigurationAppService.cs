using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using EducationAdmin.Configuration.Dto;

namespace EducationAdmin.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : EducationAdminAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
