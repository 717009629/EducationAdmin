using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using EducationAdmin.Authorization.Roles;
using EducationAdmin.Authorization.Users;
using EducationAdmin.MultiTenancy;

namespace EducationAdmin.EntityFrameworkCore
{
    public class EducationAdminDbContext : AbpZeroDbContext<Tenant, Role, User, EducationAdminDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public EducationAdminDbContext(DbContextOptions<EducationAdminDbContext> options)
            : base(options)
        {
        }
    }
}
