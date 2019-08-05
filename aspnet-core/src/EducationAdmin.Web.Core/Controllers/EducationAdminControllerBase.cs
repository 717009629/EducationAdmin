using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace EducationAdmin.Controllers
{
    public abstract class EducationAdminControllerBase: AbpController
    {
        protected EducationAdminControllerBase()
        {
            LocalizationSourceName = EducationAdminConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
