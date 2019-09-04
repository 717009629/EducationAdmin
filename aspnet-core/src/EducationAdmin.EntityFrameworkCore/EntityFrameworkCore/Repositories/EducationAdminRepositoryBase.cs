using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Repositories;
using AutoMapper;
using EducationAdmin.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationAdmin.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// Base class for custom repositories of the application.
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    /// <typeparam name="TPrimaryKey">Primary key type of the entity</typeparam>
    public abstract class EducationAdminRepositoryBase<TEntity, TPrimaryKey> : EfCoreRepositoryBase<EducationAdminDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected EducationAdminRepositoryBase(IDbContextProvider<EducationAdminDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        // Add your common methods for all repositories

        public async Task InsertListAsync(IEnumerable<TEntity> entities)
        {
            Context.AddRange(entities);
            await Context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Base class for custom repositories of the application.
    /// This is a shortcut of <see cref="EducationAdminRepositoryBase{TEntity,TPrimaryKey}"/> for <see cref="int"/> primary key.
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public abstract class EducationAdminRepositoryBase<TEntity> : EducationAdminRepositoryBase<TEntity, int>, IRepository<TEntity>
        where TEntity : class, IEntity<int>
    {
        protected EducationAdminRepositoryBase(IDbContextProvider<EducationAdminDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        // Do not add any method here, add to the class above (since this inherits it)!!!
    }
}
