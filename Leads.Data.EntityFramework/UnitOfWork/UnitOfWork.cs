using System;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
using Leads.Data.Contracts.Context;
using Leads.Data.Contracts.UnitOfWork;


namespace Leads.Data.EntityFramework.UnitOfWork
{
    public abstract class UnitOfWork<T> : IUnitOfWork where T : IDbContext
    {
        public void Dispose()
        {
            
            //if (DbContext.Database.Connection.State == ConnectionState.Open)
            //{
            //    DbContext.Database.Connection.Close();
            //}
            DbContext.Dispose();
            GC.SuppressFinalize(this);

        }

        public void SaveAnyChanges()
        {
            try
            {
                DbContext.SaveAnyChanges();
            }
            catch (DbEntityValidationException dberr)
            {
                var val = dberr.EntityValidationErrors.ToList();
                throw dberr;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public IDbContext DbContext { get; set; }
    }
}
