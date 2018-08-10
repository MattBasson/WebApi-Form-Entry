using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Leads.Data.Contracts.UnitOfWork;
using Leads.Web.APInfrastructure.Contracts;


namespace Leads.Web.APInfrastructure.Controllers
{
    public class BaseApiController : ApiController
    {
        protected readonly ILeadsUnitOfWork _LeadsUnitOfWork;
        protected readonly IApplicationSettings _settings;
        public BaseApiController(ILeadsUnitOfWork unitOfWork, IApplicationSettings settings)
        {
            _LeadsUnitOfWork = unitOfWork;
            _settings = settings;
        }
        
        protected override void Dispose(bool disposing)
        {

            _LeadsUnitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}
