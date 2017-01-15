

using Leads.Entities.Base;

namespace Leads.Data.Contracts.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public interface IDatabaseRepository<TEntity> where TEntity : EntityBase
    {
        TEntity GetById(int id);
        TEntity GetByIdIncluding(int id, params Expression<Func<TEntity, EntityBase>>[] includes);
        IQueryable<TEntity> QueryAll();
        IQueryable<TEntity> QueryAllIncluding(params Expression<Func<TEntity, EntityBase>>[] includes);
        TEntity Create(TEntity entity);
        TEntity Delete(TEntity entity);
        TEntity Update(TEntity entity);
        TEntity SaveOrUpdate(TEntity entity);
    }
}
