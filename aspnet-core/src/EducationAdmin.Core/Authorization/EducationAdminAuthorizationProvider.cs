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
            SetPermission(context, PermissionNames.Pages_Record, "Record", multiTenancySides: MultiTenancySides.Tenant);
            SetPermission(context, PermissionNames.Pages_Order, "Order", multiTenancySides: MultiTenancySides.Tenant);
            var contract = SetPermission(context, PermissionNames.Pages_Contract, "Contract", multiTenancySides: MultiTenancySides.Tenant);
            contract.CreateChildPermission(PermissionNames.Pages_Contract + ".Audite", L("Audite"), multiTenancySides: MultiTenancySides.Tenant);
            SetPermission(context, PermissionNames.Pages_Course, "Course", multiTenancySides: MultiTenancySides.Tenant);
            SetPermission(context, PermissionNames.Pages_Customer, "Customer", multiTenancySides: MultiTenancySides.Tenant);
            SetPermission(context, PermissionNames.Pages_Lesson, "Lesson", multiTenancySides: MultiTenancySides.Tenant);

            context.CreatePermission(PermissionNames.Pages_Teacher, L("Teahcer"), multiTenancySides: MultiTenancySides.Tenant);
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
