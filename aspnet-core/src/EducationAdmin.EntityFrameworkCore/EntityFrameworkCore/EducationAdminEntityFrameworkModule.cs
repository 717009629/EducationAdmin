using Abp.Dependency;
using Abp.EntityFrameworkCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Zero.EntityFrameworkCore;
using EducationAdmin.Education;
using EducationAdmin.EntityFrameworkCore.Repositories;
using EducationAdmin.EntityFrameworkCore.Seed;
using EducationAdmin.Repository;

namespace EducationAdmin.EntityFrameworkCore
{
    [DependsOn(
        typeof(EducationAdminCoreModule),
        typeof(AbpZeroCoreEntityFrameworkCoreModule))]
    public class EducationAdminEntityFrameworkModule : AbpModule
    {
        /* Used it tests to skip dbcontext registration, in order to use in-memory database of EF Core */
        public bool SkipDbContextRegistration { get; set; }

        public bool SkipDbSeed { get; set; }

        public override void PreInitialize()
        {
            if (!SkipDbContextRegistration)
            {
                Configuration.Modules.AbpEfCore().AddDbContext<EducationAdminDbContext>(options =>
                {
                    if (options.ExistingConnection != null)
                    {
                        EducationAdminDbContextConfigurer.Configure(options.DbContextOptions, options.ExistingConnection);
                    }
                    else
                    {
                        EducationAdminDbContextConfigurer.Configure(options.DbContextOptions, options.ConnectionString);
                    }                 
                });
            }
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(EducationAdminEntityFrameworkModule).GetAssembly());

            IocManager.Register<IInsertMutiRepository<LessonAttendance, long>, InsertMutiRepository<LessonAttendance, long>>(DependencyLifeStyle.Transient);
        }

        public override void PostInitialize()
        {
            if (!SkipDbSeed)
            {
                SeedHelper.SeedHostDb(IocManager);
            }
        }

    }
}
