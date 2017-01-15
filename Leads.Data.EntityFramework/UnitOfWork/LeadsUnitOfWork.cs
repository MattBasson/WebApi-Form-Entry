using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leads.Data.Contracts.Repositories;
using Leads.Data.Contracts.UnitOfWork;
using Leads.Data.EntityFramework.Context;
using Leads.Data.EntityFramework.Repositories;
using Leads.Entities.Submissions;


namespace Leads.Data.EntityFramework.UnitOfWork
{
    public class LeadsUnitOfWork : UnitOfWork<LeadsDbContext>, ILeadsUnitOfWork
    {
        public LeadsUnitOfWork()
        {
            DbContext = new LeadsDbContext();
        }
        public IDatabaseRepository<Submission> Submissions => new DatabaseRepository<Submission>(DbContext);
    }
}
