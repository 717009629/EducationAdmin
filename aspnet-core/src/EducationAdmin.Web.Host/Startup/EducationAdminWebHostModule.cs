using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using EducationAdmin.Configuration;

namespace EducationAdmin.Web.Host.Startup
{
    [DependsOn(
       typeof(EducationAdminWebCoreModule))]
    public class EducationAdminWebHostModule: AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public EducationAdminWebHostModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(EducationAdminWebHostModule).GetAssembly());
        }
    }
}
