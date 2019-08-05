using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using EducationAdmin.Configuration;
using EducationAdmin.Web;

namespace EducationAdmin.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class EducationAdminDbContextFactory : IDesignTimeDbContextFactory<EducationAdminDbContext>
    {
        public EducationAdminDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<EducationAdminDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            EducationAdminDbContextConfigurer.Configure(builder, configuration.GetConnectionString(EducationAdminConsts.ConnectionStringName));

            return new EducationAdminDbContext(builder.Options);
        }
    }
}
