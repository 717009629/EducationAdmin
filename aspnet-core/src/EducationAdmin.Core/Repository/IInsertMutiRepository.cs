using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EducationAdmin.Repository
{
    public interface IInsertMutiRepository<TEntity, TPrimaryKey>: IRepository<TEntity, TPrimaryKey> where TEntity : class,IEntity<TPrimaryKey>
    {
        Task InsertListAsync(IEnumerable<TEntity> entities);
    }
}
