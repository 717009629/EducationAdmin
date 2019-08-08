using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace EducationAdmin.Authorization
{
    public class EducationAdminAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
            context.CreatePermission(PermissionNames.Pages_Students, L("Students"));
            context.CreatePermission(PermissionNames.Pages_Record, L("Record"));
            context.CreatePermission(PermissionNames.Pages_Order, L("Order"));
            context.CreatePermission(PermissionNames.Pages_Contract, L("Contract"));
            context.CreatePermission(PermissionNames.Pages_Course, L("Course"));
            
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, EducationAdminConsts.LocalizationSourceName);
        }
    }
}
