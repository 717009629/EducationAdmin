using System.Threading.Tasks;
using Abp.Application.Services;
using EducationAdmin.Sessions.Dto;

namespace EducationAdmin.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
