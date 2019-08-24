using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Timing;
using Abp.Zero;
using Abp.Zero.Configuration;
using EducationAdmin.Authorization.Roles;
using EducationAdmin.Authorization.Users;
using EducationAdmin.Configuration;
using EducationAdmin.Localization;
using EducationAdmin.MultiTenancy;
using EducationAdmin.Timing;

namespace EducationAdmin
{
    [DependsOn(typeof(AbpZeroCoreModule))]
    public class EducationAdminCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            // Declare entity types
            Configuration.Modules.Zero().EntityTypes.Tenant = typeof(Tenant);
            Configuration.Modules.Zero().EntityTypes.Role = typeof(Role);
            Configuration.Modules.Zero().EntityTypes.User = typeof(User);

            EducationAdminLocalizationConfigurer.Configure(Configuration.Localization);

            // Enable this line to create a multi-tenant application.
            Configuration.MultiTenancy.IsEnabled = EducationAdminConsts.MultiTenancyEnabled;

            // Configure roles
            AppRoleConfig.Configure(Configuration.Modules.Zero().RoleManagement);

            Configuration.Settings.Providers.Add<AppSettingProvider>();
            Configuration.CustomConfigProviders.Add(new CustomConfigProvider());
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(EducationAdminCoreModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            IocManager.Resolve<AppTimes>().StartupTime = Clock.Now;
        }
    }
}
