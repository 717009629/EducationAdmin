using Abp.Application.Features;
using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;
using System.Collections.Generic;

namespace EducationAdmin.Authorization
{
    public class EducationAdminAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            SetPermission(context, PermissionNames.Pages_Users, "Users");
            SetPermission(context, PermissionNames.Pages_Roles, "Roles");
            SetPermission(context, PermissionNames.Pages_Tenants, "Tenants", multiTenancySides: MultiTenancySides.Host);
            SetPermission(context, PermissionNames.Pages_Students, "Students", multiTenancySides: MultiTenancySides.Tenant);
            SetPermission(context, PermissionNames.Pages_Records, "Records", multiTenancySides: MultiTenancySides.Tenant);
            SetPermission(context, PermissionNames.Pages_Orders, "Orders", multiTenancySides: MultiTenancySides.Tenant);
            var contract = SetPermission(context, PermissionNames.Pages_Contracts, "Contracts", multiTenancySides: MultiTenancySides.Tenant);
            contract.CreateChildPermission(PermissionNames.Pages_Contracts + ".Audite", L("Audite"), multiTenancySides: MultiTenancySides.Tenant);
            SetPermission(context, PermissionNames.Pages_Courses, "Courses", multiTenancySides: MultiTenancySides.Tenant);
            SetPermission(context, PermissionNames.Pages_Customers, "Customers", multiTenancySides: MultiTenancySides.Tenant);
            SetPermission(context, PermissionNames.Pages_Lessons, "Lessons", multiTenancySides: MultiTenancySides.Tenant);
            SetPermission(context, PermissionNames.Pages_Classes, "Classes", multiTenancySides: MultiTenancySides.Tenant);

            context.CreatePermission(PermissionNames.Pages_Teachers, L("Teachers"), multiTenancySides: MultiTenancySides.Tenant);
        }


        public Permission SetPermission(IPermissionDefinitionContext context, string name, string dispalyName, ILocalizableString description = null, MultiTenancySides multiTenancySides = MultiTenancySides.Tenant | MultiTenancySides.Host, IFeatureDependency featureDependency = null, Dictionary<string, object> properties = null)
        {
            var permission = context.CreatePermission(name, L(dispalyName), description, multiTenancySides, featureDependency, properties);
            permission.CreateChildPermission(name + ".Create", L("Create"), description, multiTenancySides, featureDependency, properties);
            permission.CreateChildPermission(name + ".Edit", L("Edit"), description, multiTenancySides, featureDependency, properties);
            permission.CreateChildPermission(name + ".Delete", L("Delete"), description, multiTenancySides, featureDependency, properties);
            return permission;
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, EducationAdminConsts.LocalizationSourceName);
        }
    }
}
