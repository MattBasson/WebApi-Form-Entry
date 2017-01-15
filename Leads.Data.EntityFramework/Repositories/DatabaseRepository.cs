

using Leads.Data.Contracts.Context;
using Leads.Data.Contracts.Repositories;
using Leads.Entities.Base;


namespace Leads.Data.EntityFramework.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;

    public class DatabaseRepository<TEntity> : IDatabaseRepository<TEntity>
        where TEntity : EntityBase
    {
        #region Members

        private readonly IDbContext _dbContext = null;

        #endregion

        #region Constructor

        public DatabaseRepository(IDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        #endregion

        #region IRepository<TEntity> Members

        public virtual IQueryable<TEntity> QueryAll()
        {
            return _dbContext.QueryAll<TEntity>();
        }

        public virtual IQueryable<TEntity> QueryAllIncluding(params Expression<Func<TEntity, EntityBase>>[] includes)
        {
            var all = _dbContext.QueryAll<TEntity>();

            var allIncluded = includes.Aggregate(all, (current, include) => current.Include(include));

            return allIncluded;
        }

        public virtual TEntity GetById(int id)
        {
            TEntity entity = QueryAll().SingleOrDefault(e => e.Id == id);

            return entity;
        }

        public virtual TEntity GetByIdIncluding(int id, params Expression<Func<TEntity, EntityBase>>[] includes)
        {
            TEntity entity = QueryAllIncluding(includes).SingleOrDefault(e => e.Id == id);

            return entity;
        }

        public virtual TEntity Create(TEntity entity)
        {
            if (!entity.IsTransient)
            {
                string message = string.Format("{0}.Create({1} entity) - Entity is not transient (Id={2}), use Update() or SaveOrUpdate() instead", GetType().FullName, typeof(TEntity).FullName, entity.Id);
                throw new Exception(message);
            }

            _dbContext.Add(entity);

            return entity;
        }

        public virtual TEntity Update(TEntity entity)
        {
            if (entity.IsTransient)
            {
                string message = string.Format("{0}.Update({1} entity) - Entity is transient, use Create() or SaveOrUpdate() instead", GetType().FullName, typeof(TEntity).FullName);
                throw new Exception(message);
            }

            _dbContext.Update(entity);

            return entity;
        }

        public virtual TEntity Delete(TEntity entity)
        {
            if (entity.IsTransient)
            {
                string message = string.Format("{0}.Update({1} entity) - Entity is transient, cannot delete", GetType().FullName, typeof(TEntity).FullName);
                throw new Exception(message);
            }

            _dbContext.Remove(entity);            

            return entity;
        }

        public TEntity SaveOrUpdate(TEntity entity)
        {
            return entity.IsTransient ? Create(entity) : Update(entity);
        }

        #endregion
    }
}
