using System.Data.Entity;
using Leads.Entities.Base;


namespace Leads.Data.Contracts.Context
{
    using System;
    using System.Linq;

    public interface IDbContext : IDisposable
    {
        IQueryable<TEntity> QueryAll<TEntity>() where TEntity : EntityBase;

        void SaveAnyChanges();

        void Update<TEntity>(TEntity entity) where TEntity : EntityBase;
        void Attach<TEntity>(TEntity entity) where TEntity : EntityBase;
        void Add<TEntity>(TEntity entity) where TEntity : EntityBase;
        void Remove<TEntity>(TEntity entity) where TEntity : EntityBase;
        Database Database { get; }
    }
}