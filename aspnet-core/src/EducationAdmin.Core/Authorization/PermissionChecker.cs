using Abp.Authorization;
using EducationAdmin.Authorization.Roles;
using EducationAdmin.Authorization.Users;

namespace EducationAdmin.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
