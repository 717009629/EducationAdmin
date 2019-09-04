using Abp.Domain.Entities;
using Abp.EntityFrameworkCore;
using EducationAdmin.Education;
using EducationAdmin.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.EntityFrameworkCore.Repositories
{
    public class InsertMutiRepository<TEntity, TPrimaryKey> : EducationAdminRepositoryBase<TEntity, TPrimaryKey>, IInsertMutiRepository<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        public InsertMutiRepository(IDbContextProvider<EducationAdminDbContext> dbContextProvider) : base(dbContextProvider) { }
    }
}
