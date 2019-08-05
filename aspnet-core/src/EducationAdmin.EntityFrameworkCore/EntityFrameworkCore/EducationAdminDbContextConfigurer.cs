using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace EducationAdmin.EntityFrameworkCore
{
    public static class EducationAdminDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<EducationAdminDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<EducationAdminDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
