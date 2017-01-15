using System;
using Leads.Data.Contracts.Context;


namespace Leads.Data.Contracts.UnitOfWork
{
    public interface IUnitOfWork: IDisposable
    {           
        void SaveAnyChanges();
        IDbContext DbContext { get; set; }   

    }
}
