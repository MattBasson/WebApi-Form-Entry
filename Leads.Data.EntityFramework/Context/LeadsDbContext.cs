using Leads.Data.Contracts.Context;
using Leads.Entities.Base;
using Leads.Entities.Submissions;


namespace Leads.Data.EntityFramework.Context
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Linq;
    using System.Linq.Expressions;

    public class LeadsDbContext : DbContext, IDbContext
    {

        public LeadsDbContext()
            : base("name=LeadsDbContext")
        {
        }
        #region DB Sets

        public DbSet<Submission> Submissions { get; set; }
        

        #endregion

        #region Methods

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Remove cascade deletes 
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
         
            // Stop table name pluralization
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        private void BuildManyToMany<TLeft, TRight>(DbModelBuilder modelBuilder,
                                                    Expression<Func<TLeft, ICollection<TRight>>> rightExpression,
                                                    Expression<Func<TRight, ICollection<TLeft>>> leftExpression,
                                                    string leftKey,
                                                    string rightKey,
                                                    string linkTableName)
            where TLeft : EntityBase
            where TRight : EntityBase
        {
            modelBuilder.Entity<TLeft>()
                .HasMany(rightExpression)
                .WithMany(leftExpression)
                .Map(config =>
                {
                    config.MapLeftKey(leftKey);
                    config.MapRightKey(rightKey);
                    config.ToTable(linkTableName);
                });
        }

        #endregion

        #region IDbContext Members

        public IQueryable<TEntity> QueryAll<TEntity>() where TEntity : EntityBase
        {
            return Set<TEntity>();
        }

        public void SaveAnyChanges()
        {
            try
            {
                SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Attach<TEntity>(TEntity entity) where TEntity : EntityBase
        {
            Set<TEntity>().Attach(entity);            
        }

        public void Update<TEntity>(TEntity entity) where TEntity : EntityBase
        {
            Attach(entity);
            Entry(entity).State = EntityState.Modified;
        }

        public void Add<TEntity>(TEntity entity) where TEntity : EntityBase
        {
            Set<TEntity>().Add(entity);
        }

        public void Remove<TEntity>(TEntity entity) where TEntity : EntityBase
        {
            Set<TEntity>().Remove(entity);
        }

        #endregion
    }
}
