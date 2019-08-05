using System.Threading.Tasks;
using EducationAdmin.Configuration.Dto;

namespace EducationAdmin.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
