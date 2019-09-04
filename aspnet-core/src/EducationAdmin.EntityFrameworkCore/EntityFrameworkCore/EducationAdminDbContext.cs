using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using EducationAdmin.Authorization.Roles;
using EducationAdmin.Authorization.Users;
using EducationAdmin.MultiTenancy;
using EducationAdmin.Sales;
using EducationAdmin.Education;
using EducationAdmin.Students;
using EducationAdmin.Settings;
using EducationAdmin.Migrations;

namespace EducationAdmin.EntityFrameworkCore
{
    public class EducationAdminDbContext : AbpZeroDbContext<Tenant, Role, User, EducationAdminDbContext>
    {
        /* Define a DbSet for each entity of the application */

        public EducationAdminDbContext(DbContextOptions<EducationAdminDbContext> options)
            : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }

        public DbSet<Record> Records { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Course> Courses { get; set; }

        //  public DbSet<CourseItem> CourseItems { get; set; }

        public DbSet<Contract> Contracts { get; set; }

        public DbSet<Customer> Customer { get; set; }


        public DbSet<Lesson> Lessons { get; set; }

        public DbSet<LessonAttendance> LessonAttendances { get; set; }

        public DbSet<Class> Classes { get; set; }

        public DbSet<TimePeriod> TimePeriods { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        public DbSet<Option> Options { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Record>().HasOne(m => m.Customer).WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Order>().HasOne(m => m.Student).WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Contract>().HasOne(m => m.Order).WithOne(m => m.Contract).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Contract>().HasOne(m => m.Salesman).WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Contract>().HasOne(m => m.Student).WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Lesson>().HasOne(m => m.Class).WithMany(m => m.Lessons).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Lesson>().HasOne(m => m.Teacher).WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Lesson>().HasOne(m => m.TimePeriod).WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<LessonAttendance>().HasKey(m=>new { m.LessonId,m.OrderId });
            modelBuilder.Entity<LessonAttendance>().Property(m => m.Id).UseSqlServerIdentityColumn();
            base.OnModelCreating(modelBuilder);
        }
    }
}
