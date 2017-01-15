using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leads.Data.Contracts.Repositories;
using Leads.Entities.Submissions;


namespace Leads.Data.Contracts.UnitOfWork
{
    public interface ILeadsUnitOfWork:IUnitOfWork
    {
        IDatabaseRepository<Submission> Submissions { get; }
    }
}
