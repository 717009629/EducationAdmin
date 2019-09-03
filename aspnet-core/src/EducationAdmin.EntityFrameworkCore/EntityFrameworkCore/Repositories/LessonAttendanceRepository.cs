using Abp.EntityFrameworkCore;
using EducationAdmin.Education;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.EntityFrameworkCore.Repositories
{
    public class LessonAttendanceRepository : EducationAdminRepositoryBase<LessonAttendance, long>
    {
        public LessonAttendanceRepository(IDbContextProvider<EducationAdminDbContext> dbContextProvider) : base(dbContextProvider) { }
    }
}
